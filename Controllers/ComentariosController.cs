using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FINAL_MVC.Data;
using FINAL_MVC.Models;

namespace FINAL_MVC.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly Context _context;

        public ComentariosController(Context context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            var context = _context.Comentarios.Include(c => c.Post).Include(c => c.Usuario);
            return View(await context.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.Post)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID");
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PostID,UsuarioID,Contenido,Fecha")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", comentario.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", comentario.UsuarioID);

            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", comentario.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", comentario.UsuarioID);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PostID,UsuarioID,Contenido,Fecha")] Comentario comentario)
        {
            if (id != comentario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.ID))
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
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", comentario.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", comentario.UsuarioID);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comentarios == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.Post)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comentarios == null)
            {
                return Problem("Entity set 'Context.Comentarios'  is null.");
            }
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioExists(int id)
        {

          return _context.Comentarios.Any(e => e.ID == id);

            return (_context.Comentarios?.Any(e => e.ID == id)).GetValueOrDefault();

        }
    }
}
