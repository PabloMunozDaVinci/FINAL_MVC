using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FINAL_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FINAL_MVC.Models
{
    public class TagPost
    {
        public int ID_Tag { get; set; }
        public Tag Tag { get; set; }
        public int? ID_Post { get; set; }
        public Post Post { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public TagPost() { }
    }
}
