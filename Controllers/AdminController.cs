using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FINAL_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context _context;

        public AdminController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
