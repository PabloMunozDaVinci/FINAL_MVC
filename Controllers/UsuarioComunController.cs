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
            inicializarAtributos();
        }
        public IActionResult Index()
        {
            return View();
        }

        private void inicializarAtributos()
        {
            try
            {
                _context.Usuarios.Include(u => u.MisPosts)
                   .Include(u => u.MisComentarios)
                   .Include(u => u.MisReacciones)
                   .Include(u => u.MisAmigos)
                   .Include(u => u.AmigosMios)
                   .Load();

                _context.Posts.Include(p => p.Usuario)
                    .Include(p => p.Comentarios)
                    .Include(p => p.Reacciones)
                    .Include(p => p.Tags)
                    .Load();

                _context.Comentarios.Include(c => c.Usuario)
                    .Include(c => c.Post)
                    .Load();

                _context.Tags.Include(t => t.TagPost)
                    .Include(t => t.Posts)
                    .Load();

                _context.Reacciones.Include(r => r.Usuario)
                    .Include(r => r.Post)
                    .Load();

                //Guardo los cambios 
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
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

        public IActionResult Amigos()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            _context.Usuarios.Include(u => u.MisPosts)
               .Include(u => u.MisAmigos)
               .Include(u => u.AmigosMios)
               .Load();
            string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
            //var context = _context.Usuarios.Include(u => u.AmigosMios);
            Usuario usuario = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));
            return View(usuario.MisAmigos.ToList());
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

        public async Task<IActionResult> EditarPerfil(Usuario usuario2)
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

        [HttpPost, ActionName("EliminarMiUsuario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarMiUsuario()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
            int user = Int32.Parse(usuarioId);
            var usuario = await _context.Usuarios.FindAsync(user);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Postear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Postear(string postContenido)
        {
            string usuarioId = HttpContext.Session.GetString("Usuario").ToString();
            Usuario usuariolog = _context.Usuarios.AsNoTracking().FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));
            DateTime now = DateTime.Now;
            try
            {
                if (usuariolog != null)
                {
                    Post postAux = new Post { UsuarioID = usuariolog.ID, Contenido = postContenido, Fecha = now };
                    _context.Update(postAux);
                    _context.Posts.Add(postAux);
                    usuariolog.MisPosts.Add(postAux);
                    await _context.SaveChangesAsync();
                    return View("InicioUsuario", usuariolog.MisPosts);
                }
                else
                {
                    return View("InicioUsuario", "UsuarioComun");
                }
                    
            }
            catch (Exception e)
            {
                return View("InicioUsuario", "UsuarioComun");
            }
        }


    }
}
