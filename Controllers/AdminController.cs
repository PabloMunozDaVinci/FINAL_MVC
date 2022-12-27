using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Data;
using FINAL_MVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using BCryptNet = BCrypt.Net.BCrypt;

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
            if (HttpContext.Session.GetString("Usuario") != null) {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Login()
        {
            // Almacena en usuarioId los datos de la sesión del usuario, si existen
            var usuarioId = HttpContext.Session.GetString("Usuario");
            if (usuarioId != null)
            {
                // Busca el usuario en la base de datos
                var usuario = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));

                // Valida el tipo de usuario y redirige a la vista apropiada
                return usuario.EsAdmin ? RedirectToAction("Index", "Admin") : RedirectToAction("InicioUsuario", "UsuarioComun");
            }
            else
            {
                // Si no hay usuario en la sesión, redirige al inicio de sesión
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Mail, string password)
        {
            // Busca el usuario en la base de datos
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Mail == Mail);
            if (usuario == null)
            {
                ViewBag.Error = "Su usuario y/o contraseña es incorrecta";
                return RedirectToAction("Index", "Home");
            }

            if (usuario.Bloqueado)
            {
                ViewBag.Error = "El usuario se encuentra bloqueado";
                return RedirectToAction("Index", "Home");
            }

            // Valida la contraseña del usuario
            if (!BCryptNet.Verify(password, usuario.Password))
            {
                usuario.Intentos++;
                if (usuario.Intentos == 2)
                {
                    usuario.Bloqueado = true;
                }
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();

                ViewBag.Error = "Usuario y/o contraseña incorrectos";
                return View();
            }

            usuario.Intentos = 0;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            // Inicia sesión y redirige al panel de administración o la vista de usuario común según corresponda
            HttpContext.Session.SetString("Usuario", usuario.ID.ToString());
            return usuario.EsAdmin ? RedirectToAction("Index", "Admin") : RedirectToAction("InicioUsuario", "UsuarioComun");
        }

       // [HttpPost]
        public IActionResult Logoff()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
