using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FINAL_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FINAL_MVC.Models
{
    public class UsuarioAmigo
    {
        public int ID_Usuario { get; set; }
        [ForeignKey(nameof(ID_Usuario))]
        public Usuario Usuario { get; set; }
        public int ID_Amigo { get; set; }
        [ForeignKey(nameof(ID_Amigo))]
        public Usuario Amigo { get; set; }

        //Constructor vacio para EF
        public UsuarioAmigo() { }

        public UsuarioAmigo(Usuario Principal, Usuario Segundo)
        {
            Usuario = Principal;
            Amigo = Segundo;
        }

    }
}
