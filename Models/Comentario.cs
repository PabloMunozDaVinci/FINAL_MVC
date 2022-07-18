using System;
using System.Collections.Generic;
using System.Text;

namespace FINAL_MVC.Models
{
    public class Comentario
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int UsuarioID { get; set; }
        public Post Post { get; set; } 
        public string Contenido { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }

        //Constructor vacio para EF
        public Comentario() { }

        public Comentario(int ID, int PostID, int UsuarioID, string Contenido, DateTime Fecha)
        {
            this.ID = ID;
            this.PostID = PostID;
            this.Contenido = Contenido;
            this.UsuarioID = UsuarioID;
            this.Fecha = Fecha;
        }
    }
}
