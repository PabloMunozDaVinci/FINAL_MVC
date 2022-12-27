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

                //_context.Comentarios.Include(c => c.Usuario)
                //    .Include(c => c.Post)
                //    .Load();

                //_context.Tags.Include(t => t.TagPost)
                //    .Include(t => t.Posts)
                //    .Load();

                //_context.Reacciones.Include(r => r.Usuario)
                //    .Include(r => r.Post)
                //    .Load();

                //Guardo los cambios 
                //_context.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IActionResult> InicioUsuario()
        {
            var usuarioId = HttpContext.Session.GetString("Usuario");
            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            _context.Comentarios.Include(c => c.Usuario)
               .Include(c => c.Post)
               .Load();

            var context = _context.Posts
                .Include(p => p.Usuario)
                .Include(p => p.Comentarios)
                .Include(p => p.Reacciones)
                .Include(p => p.Tags);
            var post = await context.ToListAsync();
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult>  BusquedaPorFecha(DateTime date)
        {
            var context = _context.Posts
                 .Include(p => p.Usuario)
                 .Include(p => p.Comentarios)
                 .Include(p => p.Reacciones)
                 .Include(p => p.Tags);

          
            var post = await context.ToListAsync();

            var postsPorFecha = context
                  .Where(p => p.Fecha.Date== date.Date);

        
         

            if (date != null)
            {
                //List<Post> PostsContenido = posts.AsEnumerable().ToList();
                post = await postsPorFecha.ToListAsync();
            }

            return View("InicioUsuario", post);

        }



            




        
        public async Task<IActionResult> BuscarPorUsuario( string UsuarioBuscado)
        {
            var usuarioId = HttpContext.Session.GetString("Usuario");
            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var contextUsuario = _context.Usuarios.Include(u => u.MisPosts);
            var usuario = await contextUsuario.ToListAsync();
            var usuarios = contextUsuario.Where(p => p.Nombre.ToLower().Contains(UsuarioBuscado.ToLower()));

            if (!String.IsNullOrEmpty(UsuarioBuscado))
            {
               
                usuario = await usuarios.ToListAsync();
              
            }
          
            if (usuario == null)
            {
                return NotFound();
            }
            return View("InicioUsuario", usuario);
        }


            public async Task<IActionResult> BuscarContenido(string BusquedaContenido, string UsuarioBuscado)
        {
            var usuarioId = HttpContext.Session.GetString("Usuario");
            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            _context.Comentarios.Include(c => c.Usuario)
               .Include(c => c.Post)
               .Load();

            var context = _context.Posts
                .Include(p => p.Usuario)
                .Include(p => p.Comentarios)
                .Include(p => p.Reacciones)
                .Include(p => p.Tags);

            var contextUsuario = _context.Usuarios.Include(u => u.MisPosts);

            var post = await context.ToListAsync();
            var posts = context.Where(p => p.Contenido.ToLower().Contains(BusquedaContenido.ToLower()));


            var usuario = await contextUsuario.ToListAsync();
            var usuarios = contextUsuario.Where(p => p.Nombre.ToLower().Contains(UsuarioBuscado.ToLower()));
            
            //filtro por contenido
            if (!String.IsNullOrEmpty(BusquedaContenido))
            {
               
                post = await posts.ToListAsync();
            }

        

       
            if (post == null)
            {
                return NotFound();
            }

            return View("InicioUsuario", post);
        }




















        public async Task<IActionResult> AgregarAmigo(string mailAmigo)
        {
            bool salida = false;
            string usuarioId = HttpContext.Session.GetString("Usuario").ToString();

            Usuario usuario = _context.Usuarios.FirstOrDefault(m => m.ID == Int32.Parse(usuarioId));


            foreach (Usuario a in _context.Usuarios)
            {
                if (a.Mail.Equals(mailAmigo))
                {
                    UsuarioAmigo am1 = new UsuarioAmigo(usuario, a);
                    UsuarioAmigo am2 = new UsuarioAmigo(a, usuario);
                    usuario.MisAmigos.Add(am1);
                    usuario.AmigosMios.Add(am2);
                    salida = true;
                    ViewBag.UsuarioAgregado = "Usuario agregado correctamente";
                }
            }
            if (salida)
            {
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Amigos));
            }
            // modificar esto 
            return RedirectToAction(nameof(Amigos));
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

        public async Task<IActionResult> Perfil()
        {
            var usuarioId = HttpContext.Session.GetString("Usuario");
            if (usuarioId == null)
            {
                return RedirectToAction("InicioUsuario", "UsuarioComun");
            }

            if (!int.TryParse(usuarioId, out int id))
            {
                return RedirectToAction("InicioUsuario", "UsuarioComun");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.ID == id);
            if (usuario == null)
            {
                return RedirectToAction("InicioUsuario", "UsuarioComun");
            }

            return View("Perfil", usuario);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID == id);
        }

        [HttpPost, ActionName("EditarPerfil")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(Usuario usuario2)
        {
            var usuarioId = HttpContext.Session.GetString("Usuario");
            int usuarioIdInt = Int32.Parse(HttpContext.Session.GetString("Usuario"));
            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!int.TryParse(usuarioId, out int id))
            {
                return RedirectToAction("Index", "Home");
            }

            var usuariolog = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(m => m.ID == usuarioIdInt);
            if (usuariolog == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                usuario2.Password = BCryptNet.HashPassword(usuario2.Password);
                _context.Attach(usuario2).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewBag.AvisoEdit = "Usuario editado";
                return View("Perfil", usuario2);
            }

            return View("Perfil", "UsuarioComun");
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
                    var context = _context.Posts.Include(p => p.Usuario)
                    .Include(p => p.Comentarios)
                    .Include(p => p.Reacciones)
                    .Include(p => p.Tags);

                    return View("InicioUsuario", await context.ToListAsync());
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

        [HttpPost, ActionName("Comentar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comentar(string comentarioContenido, int postID)
        {
            try
            {
                DateTime now = DateTime.Now;
                string usuarioIdString = HttpContext.Session.GetString("Usuario").ToString();
                Usuario usuariolog = _context.Usuarios.AsNoTracking().FirstOrDefault(m => m.ID == Int32.Parse(usuarioIdString));
                int usuarioId = Int32.Parse(HttpContext.Session.GetString("Usuario"));

                if (usuariolog != null)
                {

                    Comentario comentarioAux = new Comentario { PostID = postID, UsuarioID = usuarioId, Contenido = comentarioContenido, Fecha = now };
                    _context.Comentarios.Add(comentarioAux);
                    usuariolog.MisComentarios.Add(comentarioAux);
                    await _context.SaveChangesAsync();
                    var context = _context.Posts.Include(p => p.Usuario)
                    .Include(p => p.Comentarios)
                    .Include(p => p.Reacciones)
                    .Include(p => p.Tags);
                    return View("InicioUsuario", await context.ToListAsync());
                }
                
                else
                    return View("InicioUsuario", "UsuarioComun");
            }
            catch (Exception e)
            {
                return View("InicioUsuario", "UsuarioComun");
            }
        }

        [HttpPost, ActionName("Reaccionar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reaccionar(int postID)
        {
            try
            {
                string usuarioIdString = HttpContext.Session.GetString("Usuario").ToString();
                Usuario usuariolog = _context.Usuarios.AsNoTracking().FirstOrDefault(m => m.ID == Int32.Parse(usuarioIdString));
                int usuarioId = Int32.Parse(HttpContext.Session.GetString("Usuario"));

                if (usuariolog != null)
                {
                    Reaccion Reaccion = new Reaccion {Tipo = '1', PostID = postID, UsuarioID = usuarioId};
                    _context.Reacciones.Add(Reaccion);
                    usuariolog.MisReacciones.Add(Reaccion);
                    await _context.SaveChangesAsync();
                    var context = _context.Posts.Include(p => p.Usuario)
                    .Include(p => p.Comentarios)
                    .Include(p => p.Reacciones)
                    .Include(p => p.Tags);
                    return View("InicioUsuario", await context.ToListAsync());
                }
                else
                    return View("InicioUsuario", "UsuarioComun");
            }
            catch (Exception e)
            {
                return View("InicioUsuario", "UsuarioComun");
            }
        }

    }
}
