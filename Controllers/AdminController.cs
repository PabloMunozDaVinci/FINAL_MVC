using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using FINAL_MVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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



        public IActionResult Login()
        {
            //Consulto si en la session existe algo guardado en usuario
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                //Guardo en usuarioID los datos de session del usuario
                string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
                Usuario usuario = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));

                //validacion de tipo de usuario para dirigir a la vista pertinente
                if (usuario.EsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("InicioUsuario", "Posts");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Mail, string password)
        {
            Usuario usuario = _context.Usuarios.Where(U => U.Mail == Mail).FirstOrDefault();


            if (usuario != null)
            { //msg usuario valio

                HttpContext.Session.SetString("Usuario", usuario.ID.ToString());

                if (!usuario.Bloqueado)
                {
                    //preguntar si el usuario no está bloqueado
                    if (usuario.Password == password)
                    {
                        if (usuario.Intentos != 0)
                        {
                            usuario.Intentos = 0;
                            _context.Usuarios.Update(usuario);
                            _context.SaveChanges();
                        }
                        if (usuario.EsAdmin)
                        {
                            //usuario es admin
                            HttpContext.Session.SetString("Usuario", usuario.ID.ToString());
                            HttpContext.Session.SetString("UsuarioLogueadoAdmin", usuario.ID.ToString());






                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            //usuario es cliente
                            HttpContext.Session.SetString("Usuario", usuario.ID.ToString());
                            HttpContext.Session.SetString("UsuarioLogueado", usuario.ID.ToString());
                            return RedirectToAction("inicioUsuario", "Posts");
                        }
                    }
                    else
                    {
                        //usuario valio, contraseña invalida
                        usuario.Intentos++;

                        if (usuario.Intentos == 2)
                        {
                            usuario.Bloqueado = true;
                        }
                        _context.Usuarios.Update(usuario);
                        _context.SaveChanges();

                        ViewBag.Error = "Usuario y/o contraseña  incorrectos";

                        return View();
                        //contador de intentos de login
                    }
                }
                else
                {
                    //mensaje busuario bloqueado

                    ViewBag.Error = "El usuario se encuentra bloqueado";

                    return View();
                }
            }
            else
            {
                //msg usuario/contraseña no validos

                ViewBag.Error = "Su usuario y/o contraseña es incorrecta";

                return RedirectToAction("Index", "Home");

            }
        }


        [HttpPost]
        public IActionResult Logoff()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
