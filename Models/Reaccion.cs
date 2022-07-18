using System;
using System.Collections.Generic;
using System.Text;

namespace FINAL_MVC.Models
{
    public class Reaccion
    {
        public int ID { get; set; }
        public char Tipo { get; set; }
        public int PostID { get; set; }
        public int UsuarioID { get; set; }
        public Post Post { get; set; }
        public Usuario Usuario { get; set; }

        //Constructor vacio para EF
        public Reaccion() { }

        public Reaccion(int ID, char Tipo,int PostID,int UsuarioID) {
           
            this.ID = ID;
            this.Tipo = Tipo;
            this.PostID = PostID;
            this.UsuarioID = UsuarioID;
        }
    }
}
