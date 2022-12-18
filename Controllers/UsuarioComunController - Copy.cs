using FINAL_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net.BCrypt;

namespace FINAL_MVC.Controllers
{
    public class UsuarioComunController : Controller
    {
        private readonly Context _context;

        public UsuarioComunController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                usuario.Password = BCryptNet.HashPassword(usuario.Password);
                await _context.SaveChangesAsync();
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
