using System;
using System.Collections.Generic;
using System.Text;
using FINAL_MVC.Models;

namespace FINAL_MVC.Models
{
     public class Usuario
    {
        public int ID { get; set;}
        public string Nombre{ get; set; }
        public string Apellido  { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool EsAdmin { get; set; }
        public bool Bloqueado { get; set; }
        public int Intentos { get; set; }
        public List <Post> MisPosts { get; set; } = new List<Post>();
        public List<Comentario> MisComentarios { get; set; } = new List<Comentario>();
        public List<Reaccion> MisReacciones { get; set; } = new List<Reaccion>();
        public virtual ICollection<UsuarioAmigo>? MisAmigos { get; set; }
        public virtual ICollection<UsuarioAmigo>? AmigosMios { get; set; }


        public Usuario() { }

        //Constructor logico para registrar un usuario
        public Usuario(string Nombre, string Apellido, string Mail, string Password, int Intentos)
        {            
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Mail = Mail;
            this.Password = Password;
            Bloqueado = false;
            EsAdmin = false;
            this.Intentos = Intentos;
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
