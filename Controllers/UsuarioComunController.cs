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

        public IActionResult InicioUsuario()
        {
            return View();
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
                    try
                    {
                        _context.Update(usuario2);
                        await _context.SaveChangesAsync();
                    }catch (DbUpdateConcurrencyException){
                        if (!UsuarioExists(usuario2.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(usuario2);
            }
            else{
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
