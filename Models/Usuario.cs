using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using FINAL_MVC.Models;

namespace FINAL_MVC.Models
{
     public class Usuario
    {
        public int ID { get; set;}
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener 2 caracteres como mínimo")]
        public string Nombre{ get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener 2 caracteres como mínimo")]
        public string Apellido  { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "El correo es inválido")]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(500, MinimumLength = 8, ErrorMessage = "La contraseña debe tener 8 caracteres como mínimo")]
        public string Password { get; set; }
        public bool EsAdmin { get; set; }
        public bool Bloqueado { get; set; }
        public List <Post> MisPosts { get; set; } = new List<Post>();
        public List<Comentario> MisComentarios { get; set; } = new List<Comentario>();
        public List<Reaccion> MisReacciones { get; set; } = new List<Reaccion>();
        public virtual ICollection<UsuarioAmigo> MisAmigos { get; set; }
        public virtual ICollection<UsuarioAmigo> AmigosMios { get; set; }


        public Usuario() { }

        //Constructor logico para registrar un usuario
        public Usuario(string Nombre, string Apellido, string Mail, string Password)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Mail = Mail;
            this.Password = Password;
            Bloqueado = false;
            EsAdmin = false;            
            MisPosts = new List<Post>();
            MisComentarios = new List<Comentario>();
            MisReacciones = new List<Reaccion>();

        }

        public string[] toArray()
        {
            return new string[] { Nombre, Apellido, Mail, Bloqueado.ToString() };
        }

    }
}
