using FINAL_MVC.Data;
using FINAL_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace FINAL_MVC.Controllers
{


  
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    namespace FINAL_MVC.Controllers
    {
        public class HomeController : Controller
        {
            private readonly Context _context;

            public HomeController(Context context)
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
                        return RedirectToAction("Index", "HomeController");
                    }
                    else
                    {
                        return RedirectToAction("Index", "HomeController");
                    }
                }
                else
                {
                    return View("Index");
                }

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Login(string Mail, string password)
            {
                Usuario usuario = _context.Usuarios.Where(U => U.Mail == Mail).FirstOrDefault();


                if (usuario != null)
                { //msg usuario valio
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
                                return RedirectToAction("InicioUsuario", "Posts");
                            }
                            else
                            {
                                //usuario es cliente
                                HttpContext.Session.SetString("Usuario", usuario.ID.ToString());
                                return RedirectToAction("InicioUsuario", "Posts");
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

                            ViewBag.Errror = "Su usuario y/o contraseña es incorrecta";

                            return View();
                            //contador de intentos de login
                        }
                    }
                    else
                    {
                        //mensaje busuario bloqueado

                        ViewBag.Errror = "Su usuario se encuentra bloqueado";

                        return View();
                    }
                }
                else
                {
                    //msg usuario/contraseña no validos

                    ViewBag.Errror = "Su usuario y/o contraseña es incorrecta";

                    return View();

                }
            }

            [HttpPost]
            public IActionResult Logoff()
            {

                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //public IActionResult Error()
            //{
            //  return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
    }
}
