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

        [HttpPost]
        public IActionResult EditarPerfil(Usuario usuarioEdit)
        {
            if (HttpContext.Session.GetString("Usuario").ToString() != null)
            {
                string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
                //Usuario usuario = _context.Usuario.FirstOrDefault(m => m.id == Int32.Parse(usuarioId));
                //info
                if (ModelState.IsValid)
                {
                    Usuario user = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));
                    string pass = user.Password;
                    user = null;
                    _context.Update(usuarioEdit);
                    _context.SaveChanges();
                    ViewBag.AvisoEdit = "Usuario editado";
                    return View("Perfil", usuarioEdit);
                }
                else
                {
                    return ViewBag.AvisoEdit = "Error";
                }
            }
            else
            {
                return View("InicioUsuario", "UsuarioComun");
            }
        }


    }
}
