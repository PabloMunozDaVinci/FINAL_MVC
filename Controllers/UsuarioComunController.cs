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

        public async Task<IActionResult> InicioUsuario()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Index", "Home");

            }
            var context = _context.Posts.Include(p => p.Usuario);
            return View(await context.ToListAsync());
        }

      

        public IActionResult Perfil()
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
                Usuario usuario = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));
                return View("Perfil", usuario);
            }
            else
            {
                return RedirectToAction("InicioUsuario", "UsuarioComun");
            }
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditarPerfil(int id, Usuario usuario2)
        {
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
                Usuario usuariolog = _context.Usuarios.AsNoTracking().FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));
                if (ModelState.IsValid)
                {
                    usuario2.Password = BCryptNet.HashPassword(usuario2.Password);
                   
                    try
                    {
                        _context.Update(usuario2);
                        await _context.SaveChangesAsync();
                        ViewBag.AvisoEdit = "Usuario editado";
                    }
                    catch (DbUpdateConcurrencyException){
                        if (!UsuarioExists(usuario2.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return View("Perfil",usuario2);
                }
                return View(usuario2);
            }
            else{
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarMiUsuario()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
