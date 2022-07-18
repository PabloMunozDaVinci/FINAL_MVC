using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FINAL_MVC.Models
{
    public class Tag
    {
        public int ID    { get; set; }
        public string Palabra { get; set; }
        //public List<Post> Posts { get; set; }
        public List<TagPost> TagPost { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        //Constructor vacio para EF
        public Tag(){ }

        public Tag(int ID, string Palabra) {
         
            this.ID = ID;
            this.Palabra = Palabra;
        }
    }
}
