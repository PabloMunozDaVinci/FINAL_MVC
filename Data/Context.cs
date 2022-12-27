using FINAL_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FINAL_MVC.Data
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reaccion> Reacciones { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public Context(DbContextOptions<Context> optionsBuilder) : base(optionsBuilder) { }
        public Context() { }
        DateTime now = DateTime.Now;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MyContext");
            optionsBuilder.UseSqlServer(connectionString);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //==================== CREACION DE LAS TABLAS ============================
            // TABLA Usuario           
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.ID);
            // TABLA Reaccion 
            modelBuilder.Entity<Reaccion>()
                .ToTable("Reaccion")
                .HasKey(r => r.ID);
            // TABLA Post 
            modelBuilder.Entity<Post>()
                .ToTable("Post")
                .HasKey(p => p.ID);
            // TABLA Comentario 
            modelBuilder.Entity<Comentario>()
                .ToTable("Comentario")
                .HasKey(c => c.ID);
            // TABLA Tag 
            modelBuilder.Entity<Tag>()
                .ToTable("Tag")
                .HasKey(t => t.ID);



            //==================== RELACIONES ============================

            //DEFINICIÓN DE RELACIONES ONE TO MANY

            // UsuarioAmigo
            modelBuilder.Entity<UsuarioAmigo>()
                .HasOne(UA => UA.Usuario)
                .WithMany(U => U.MisAmigos)
                .HasForeignKey(U => U.ID_Usuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsuarioAmigo>()
                .HasOne(UA => UA.Amigo)
                .WithMany(U => U.AmigosMios)
                .HasForeignKey(U => U.ID_Amigo)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UsuarioAmigo>().HasKey(k => new { k.ID_Usuario, k.ID_Amigo });

            // Comentario-Usuario
            modelBuilder.Entity<Comentario>()
                .HasOne(C => C.Usuario)
                .WithMany(U => U.MisComentarios)
                .HasForeignKey(C => C.UsuarioID)
                 .OnDelete(DeleteBehavior.Cascade);

            // Reacciones-Usuario
            modelBuilder.Entity<Reaccion>()
                .HasOne(R => R.Usuario)
                .WithMany(U => U.MisReacciones)
                .HasForeignKey(R => R.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            // Post-Usuario
            modelBuilder.Entity<Post>()
                .HasOne(P => P.Usuario)
                .WithMany(U => U.MisPosts)
                .HasForeignKey(P => P.UsuarioID)
                .OnDelete(DeleteBehavior.Cascade);

            // Comentario-Post
            modelBuilder.Entity<Comentario>()
                .HasOne(C => C.Post)
                .WithMany(P => P.Comentarios)
                .HasForeignKey(C => C.PostID)
                .OnDelete(DeleteBehavior.Restrict);

            // Reaccion-Post
            modelBuilder.Entity<Reaccion>()
                .HasOne(R => R.Post)
                .WithMany(P => P.Reacciones)
                .HasForeignKey(R => R.PostID)
               .OnDelete(DeleteBehavior.Restrict);


            //DEFINICIÓN DE LAS RELACIONES MANY TO MANY

            // Post-Tag
            modelBuilder.Entity<Post>()
                .HasMany(P => P.Tags)
                .WithMany(T => T.Posts)
                .UsingEntity<TagPost>(
                    etp => etp.HasOne(TP => TP.Tag).WithMany(T => T.TagPost).HasForeignKey(P => P.ID_Tag),
                    etp => etp.HasOne(TP => TP.Post).WithMany(P => P.TagPost).HasForeignKey(P => P.ID_Post),
                    etp => etp.HasKey(k => new { k.ID_Tag, k.ID_Post })
                );


            //==================== propiedades de los datos ============================

            //Usuario
            modelBuilder.Entity<Usuario>
                (
                usuario =>
                {
                    usuario.Property(u => u.Nombre).HasColumnType("varchar(50)");
                    usuario.Property(u => u.Apellido).HasColumnType("varchar(50)");
                    usuario.Property(u => u.Mail).HasColumnType("varchar(50)");
                    usuario.Property(u => u.Password).HasColumnType("varchar(MAX)");
                    usuario.Property(u => u.EsAdmin).HasColumnType("bit");
                    usuario.Property(u => u.Bloqueado).HasColumnType("bit");
                    usuario.Property(u => u.Intentos).HasColumnType("int");
                });

            //Comentario
            modelBuilder.Entity<Comentario>(
                comentario =>
                {
                    comentario.Property(c => c.Contenido).HasColumnType("varchar(350)");
                    comentario.Property(c => c.Fecha).HasColumnType("DateTime");
                });

            //Reaccion
            modelBuilder.Entity<Reaccion>(
                reaccion =>
                {
                    reaccion.Property(r => r.Tipo).HasColumnType("varchar(5)");
                });

            //Post
            modelBuilder.Entity<Post>(
                post =>
                {
                    post.Property(p => p.Contenido).HasColumnType("varchar(350)");
                    post.Property(p => p.Fecha).HasColumnType("datetime");
                });

            //Tag
            modelBuilder.Entity<Tag>(
                tag =>
                {
                    tag.Property(t => t.Palabra).HasColumnType("varchar(10)");
                });

            //Ignoro, no agrego UsuarioManager a la base de datos , ni DB_Management
     

            //AGREGO ALGUNOS DATOS DE PRUEBA
            modelBuilder.Entity<Usuario>().HasData(
                new { ID = 1, Nombre = "administrador", Apellido = "adminApellido", Mail = "administrador@gmail.com", Password = BCryptNet.HashPassword("administrador"), EsAdmin = true, Bloqueado = false, Intentos = 0 },
                new { ID = 2, Nombre = "usuario1", Apellido = "usuario1Apellido", Mail = "usuario1@gmail.com", Password = BCryptNet.HashPassword("usuario1") , EsAdmin = false, Bloqueado = false, Intentos = 0 },
                new { ID = 3, Nombre = "usuario2", Apellido = "usuario2Apellido", Mail = "usuario2@gmail.com", Password = BCryptNet.HashPassword("usuario2"), EsAdmin = false, Bloqueado = false, Intentos = 0 });
           

            modelBuilder.Entity<Post>().HasData(
                new { ID = 1, UsuarioID = 2, Contenido = "Esto es un post de prueba", Fecha = now },
                new { ID = 2, UsuarioID = 3, Contenido = "lorem ipsum dolor amet", Fecha = now },
                new { ID = 3, UsuarioID = 2, Contenido = "messi campeon mundial , lleva a argentina a la victora", Fecha = now },
                new { ID = 4, UsuarioID = 3, Contenido = "crisis economica en venezuela , se complica la situacion", Fecha = now },
                new { ID = 5, UsuarioID = 2, Contenido = "las mejores recetas de cocina en nuestra web", Fecha = now });

            modelBuilder.Entity<Comentario>().HasData(
                 
                 new { ID = 1, PostID = 1, UsuarioID = 2, Contenido = "comentario aleatorio",Fecha=now},
                 new { ID = 2, PostID = 2, UsuarioID = 3, Contenido = "comentario aleatorio1", Fecha = now },
                 new { ID = 3, PostID = 1, UsuarioID = 2, Contenido = "comentario aleatorio2", Fecha = now },
                 new { ID = 4, PostID = 4, UsuarioID = 3, Contenido = "comentario aleatorio3", Fecha = now },
                 new { ID = 5, PostID = 5, UsuarioID = 2, Contenido = "comentario aleatorio4", Fecha = now }
                ); ;

            //modelBuilder.Entity<Reaccion>().HasData(

            //    new { ID = 1, Tipo = "a", PostID = 1, UsuarioID = 2 },
            //    new { ID = 2, Tipo = "a", PostID = 2, UsuarioID = 3 },
            //    new { ID = 3, Tipo = "a", PostID = 4, UsuarioID = 3 },
            //    new { ID = 4, Tipo = "a", PostID = 5, UsuarioID = 2 },
            //    new { ID = 5, Tipo = "a", PostID = 3, UsuarioID = 2 }
            //    );

            modelBuilder.Entity<Tag>().HasData(

               new { ID = 1,Palabra  = "messi" },
               new { ID = 2, Palabra = "Comida" },
               new { ID = 3, Palabra = "politica" },
               new { ID = 4, Palabra = "religion" },
               new { ID = 5, Palabra = "deporte" }
               );


        }
        public int ID { get; set; }
        public int PostID { get; set; }
        public int UsuarioID { get; set; }
        public Post? Post { get; set; }
        public string Contenido { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime Fecha { get; set; }

    }
}
