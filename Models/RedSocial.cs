using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyEncryption;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FINAL_MVC.Data;
namespace FINAL_MVC.Models
{
    public class RedSocial
    {
        public IDictionary<string, int> loginHistory;
        private const int cantMaxIntentos = 3;
        private Context context;
        DateTime now = DateTime.Now;
        public Usuario usuarioActual { get; set; }
        public RedSocial()
        {
            this.usuarioActual = usuarioActual;
            this.loginHistory = new Dictionary<string, int>();
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            try
            {
                // creo el contexto 
                context = new Context();

                context.Usuarios.Include(u => u.MisPosts)
                   .Include(u => u.MisComentarios)
                   .Include(u => u.MisReacciones)
                   .Include(u => u.MisAmigos)
                   .Include(u => u.AmigosMios)
                   .Load();

                context.Posts.Include(p => p.Usuario)
                    .Include(p => p.Comentarios)
                    .Include(p => p.Reacciones)
                    .Include(p => p.Tags)
                    .Load();

                context.Comentarios.Include(c => c.Usuario)
                    .Include(c => c.Post)
                    .Load();

                context.Tags.Include(t => t.TagPost)
                    .Include(t => t.Posts)
                    .Load();

                context.Reacciones.Include(r => r.Usuario)
                    .Include(r => r.Post)
                    .Load();

                //Guardo los cambios 
                context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public bool Cerrar()
        {
            context.Dispose();
            return true;
        }

        //metodo para hashear la contraseña
        private string Hashear(string contraseñaSinHashear)
        {
            try
            {
                string passwordHash = SHA.ComputeSHA256Hash(contraseñaSinHashear);
                return passwordHash;
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public string Intentos(string usuarioIngresado)
        {
            string mensaje = null;
            if (loginHistory.TryGetValue(usuarioIngresado, out int value))
            {
                loginHistory[usuarioIngresado] = loginHistory[usuarioIngresado] + 1;
                mensaje = "Datos incorrectos, intento " + loginHistory[usuarioIngresado] + "/3";
                if (loginHistory[usuarioIngresado] == cantMaxIntentos)
                {
                    this.bloquearDesbloquearUsuario(usuarioIngresado, true);
                    mensaje = "Intento 3/3, usuario bloqueado.";
                }
            }
            else
            {
                mensaje = "Datos incorrectos, intento 1/3";
                loginHistory.Add(usuarioIngresado, 1);
            }
            return mensaje;
        }

        public bool EstaBloqueado(string Mail)
        {
            return DevolverUsuario(Mail).Bloqueado;
        }

        // Modificar los datos del usuario logeado
        public bool ModificarUsuario(string newNombre, string newApellido, string newMail, string newPassword)
        {
            try
            {
                if(newNombre != "")
                {
                    newNombre = usuarioActual.Nombre = newNombre;
                }
                else
                {
                    newNombre = usuarioActual.Nombre;
                }
                if (newApellido != "")
                {
                    newApellido = usuarioActual.Apellido = newApellido;
                }
                else
                {
                    newApellido = usuarioActual.Apellido;
                }
                if (newMail != "")
                {
                    newMail = usuarioActual.Mail = newMail;
                }
                else
                {
                    newMail = usuarioActual.Mail;
                }
                if (newPassword != "")
                {
                    newPassword = usuarioActual.Password = newPassword;
                }
                else
                {
                    newPassword = usuarioActual.Password;
                }
                context.Usuarios.Update(usuarioActual);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ModificarUsuarioAdmin(string newNombre, string newApellido, string newMail, string EmailEncontrado)
        {
            bool salida = false;

            foreach (Usuario u in context.Usuarios)
            {
                if (u.Mail.Equals(EmailEncontrado))
                {
                    if (newNombre != "")
                    {
                        u.Nombre = newNombre;
                    }
                    else
                    {
                        newNombre = u.Nombre;
                    }
                    if (newApellido != "")
                    {
                        u.Apellido = newApellido;
                    }
                    else
                    {
                        newApellido = u.Apellido;
                    }
                    if (newMail != "")
                    {
                        u.Mail = newMail;
                    }
                    else
                    {
                        newMail = u.Mail;
                    }

                    context.Usuarios.Update(u);
                    
                    salida = true;
                }
              
            }
            return salida;
            context.SaveChanges();
        }

        // Elimina al usuario logueado
        public bool EliminarUsuario()
        {
            try
            {
                bool salida = false;                 
                context.Usuarios.Remove(usuarioActual);
                context.SaveChanges();

                return salida;                                    
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Devuelve el Usuario correspondiente al Mail recibido.
        public Usuario DevolverUsuario(string Mail)
        {
            //Usuario usuarioEncontrado = null;
            return context.Usuarios.Where(U => U.Mail == Mail).FirstOrDefault();
        }

        // Se registra un nuevo usuario
        public bool RegistrarUsuario(string Nombre, string Apellido, string Mail, string Password, bool EsAdmin, bool Bloqueado)
        {
            try
            {
                Usuario nuevo = new Usuario { Nombre = Nombre, Apellido = Apellido, Mail = Mail, Password = Password, EsAdmin = EsAdmin, Bloqueado = Bloqueado };
                context.Usuarios.Add(nuevo);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        // Se autentica al Usuario.
        public bool IniciarUsuario(string Mail, string Password)
        {
            bool ok = false;
            Usuario usuario = DevolverUsuario(Mail);
            if (usuario.Password == Password)
            {
                usuarioActual = usuario;
                ok = true;
            }
            return ok;
        }

        // Se valida si el usuario existe y devuelve true o false
        public bool ExisteUsuario(string Mail)
        {
            if (DevolverUsuario(Mail) != null)
            {
                return true;
            }
            return false;
        }

        // Bloquea/Desbloquea el Usuario que se corresponde con el DNI recibido.
        public bool bloquearDesbloquearUsuario(string Mail, bool Bloqueado)
        {
            bool todoOk = false;
            foreach (Usuario u in context.Usuarios)
            {
                if (u.Mail == Mail)
                {
                    u.Bloqueado = Bloqueado;
                    todoOk = true;
                }
            }
            return todoOk;
        }

        // Cierra la sesion 
        public bool CerrarSesion()
        {
            //Pregunto si existe usuario Actual
            if (usuarioActual != null)
            {
                //seteo el usuario actual a null
                usuarioActual = null;
                context.Dispose();
            }
            return true;
        }

        // no se si funciona
        public bool AgregarAmigo(string mailAmigo)
        {
            bool salida = false;
            foreach (Usuario a in context.Usuarios)
            {
                if (a.Mail.Equals(mailAmigo))
                {
                    UsuarioAmigo am1 = new UsuarioAmigo(usuarioActual, a);
                    UsuarioAmigo am2 = new UsuarioAmigo(a, usuarioActual);
                    usuarioActual.MisAmigos.Add(am1);
                    usuarioActual.AmigosMios.Add(am2);
                    salida = true;
                }                          
            }
            if (salida)
            {
                context.SaveChanges();
                return salida;
            }

            return salida;
        }

        // no funciona
        public void QuitarAmigo(Usuario exAmigo)
        {
            if (usuarioActual != null)
            {

                UsuarioAmigo am1 = usuarioActual.MisAmigos.Where(ua => ua.ID_Usuario == usuarioActual.ID).Where(ua => ua.ID_Amigo == exAmigo.ID).FirstOrDefault();

                usuarioActual.MisAmigos.Remove(am1);
                context.Usuarios.Update(usuarioActual);
                context.SaveChanges();




            }
        }

        // Metodo para agregar un nuevo Post
        public bool Postear(int userID, String postContenido)
        {
            DateTime now = DateTime.Now;
            try
            {
                Usuario usrAux = usuarioActual;

                if (usrAux != null)
                {
                    //revisar para pasar referencia  en usuario
                    Post postAux = new Post { UsuarioID = usuarioActual.ID, Contenido = postContenido, Fecha = now };

                    context.Posts.Add(postAux);
                    usrAux.MisPosts.Add(postAux);
                    context.Usuarios.Update(usrAux);

                    context.SaveChanges();

                    return true;





                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }








     















        // Metodo para agregar un comentario
        public bool Comentar(String comentarioContenido, int postID)
        {
            try
            {
                DateTime now = DateTime.Now;
                Usuario usrAux = usuarioActual;

                if (usrAux != null)
                {
                    //revisar 
                    Comentario comentarioAux = new Comentario { PostID= postID ,Usuario = usuarioActual, Contenido = comentarioContenido, Fecha = now };

                    context.Comentarios.Add(comentarioAux);
                    usrAux.MisComentarios.Add(comentarioAux);


                    context.SaveChanges();

                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Elimina un post, y todos sus comentarios
        public bool EliminarPost(int pID)
        {




            bool salida = false; 
            foreach (Post p in context.Posts)
            {
                if (p.ID == pID)
                {
                    foreach(Comentario c in p.Comentarios)
                    {

                        context.Comentarios.Remove(c);
                    }
                    context.Posts.Remove(p);
                    salida = true;
                }
            }
            if (salida)
            {
                context.SaveChanges();
            }                
            return salida;
        }


        public bool EliminarComentario(int cID)
        {
            bool salida = false;
            foreach (Comentario c in context.Comentarios) { 
            
                if (c.ID == cID)
                {

                    context.Comentarios.Remove(c);
                    salida = true;
                }
            }
            if (salida)
            {
                context.SaveChanges();
            }
            return salida;
        }
        public bool ModificarPost(int pID, string pContenido)
        {
            bool salida = false;
            foreach (Post post in context.Posts)
            
                if (post.ID == pID)
                {
                 
                    post.Contenido = pContenido;
                
                    post.Fecha = now;
                    context.Update(post);
                    

                    salida = true;
                }
                if (salida)
                    context.SaveChanges();
                return salida;
            
        }
        public bool ModificarComentario(int cID,string cContenido)
        {
            bool salida = false;
            foreach (Comentario c in context.Comentarios)

                if (c.ID == cID)
                {

                    c.Contenido = cContenido;
                    c.Fecha = now;
                    context.Update(c);


                    salida = true;
                }
            if (salida)
                context.SaveChanges();
            return salida;
        }

        public List<Post> obtenerPostPorContenido(string Contenido)
        {

            List<Post> salida = new List<Post>();
            
            var query = context.Posts.Where(p => p.Contenido.Contains(Contenido));
          //var query = context.Posts.Where(p => EF.Functions.Like(p.Contenido,"%"+Contenido+"%"));
          //  var query = context.Posts.Where(p => EF.Functions.Like(p.Contenido, "[" + Contenido + "]%"));
            foreach (Post p in query)
                salida.Add(p);
                    
            return salida;
        }

        public List<Post> obtenerPosts()
        {
            return  context.Posts.ToList();
        }

        public List<Comentario> obtenerComentarios()
        {
            return context.Comentarios.ToList();

        }

        public List<Reaccion> obtenerReacciones()
        {
            return context.Reacciones.ToList();
        }

        public List<Tag> obtenerTags()
        {
            return context.Tags.ToList();
        }

        public List<Usuario> obtenerUsuarios()
        {
            return context.Usuarios.ToList();
        }

        public void Reaccionar(Post p, Reaccion r)
        {

        }

        public void ModificarReaccion(Post p, Reaccion r)
        {

        }

        public void QuitarReaccion(Post p, Reaccion r)
        {

        }

        public void MostrarDatos()
        {

        }

        public void MostrarPosts()
        {

        }

        public void MostrarPostsAmigos()
        {

        }

        public void BuscarPosts(string Contenido, DateTime Fecha, Tag t)
        {

        }

        // Metodo para eliminar usuarios siendo el usuario administrador
        public bool EliminarUsuarioAdmin(string Mail)
        {
            try
            {
                bool salida = false;
                foreach (Usuario u in context.Usuarios)
                    if (u.Mail == Mail)
                    {
                        context.Usuarios.Remove(u);
                        salida = true;
                    }
                if (salida)
                    context.SaveChanges();
                return salida;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Obtiene 1 usuario en cuestion
        public Usuario obtenerUsuario(string nombre)
        {
            return context.Usuarios.Where(U => U.Nombre == nombre).FirstOrDefault();
        }


    }
}
