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
    /*public class UsuarioAmigoesController : Controller
    {
        private readonly Context _context;

        public UsuarioAmigoesController(Context context)
        {
            _context = context;
        }

        // GET: UsuarioAmigoes
        public async Task<IActionResult> Index()
        {
            var context = _context.UsuarioAmigo.Include(u => u.Amigo).Include(u => u.Usuario);
            return View(await context.ToListAsync());
        }

        // GET: UsuarioAmigoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioAmigo == null)
            {
                return NotFound();
            }

            var usuarioAmigo = await _context.UsuarioAmigo
                .Include(u => u.Amigo)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.ID_Usuario == id);
            if (usuarioAmigo == null)
            {
                return NotFound();
            }

            return View(usuarioAmigo);
        }

        // GET: UsuarioAmigoes/Create
        public IActionResult Create()
        {
            ViewData["ID_Amigo"] = new SelectList(_context.Usuarios, "ID", "ID");
            ViewData["ID_Usuario"] = new SelectList(_context.Usuarios, "ID", "ID");
            return View();
        }

        // POST: UsuarioAmigoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Usuario,ID_Amigo")] UsuarioAmigo usuarioAmigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioAmigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_Amigo"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Amigo);
            ViewData["ID_Usuario"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Usuario);
            return View(usuarioAmigo);
        }

        // GET: UsuarioAmigoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioAmigo == null)
            {
                return NotFound();
            }

            var usuarioAmigo = await _context.UsuarioAmigo.FindAsync(id);
            if (usuarioAmigo == null)
            {
                return NotFound();
            }
            ViewData["ID_Amigo"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Amigo);
            ViewData["ID_Usuario"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Usuario);
            return View(usuarioAmigo);
        }

        // POST: UsuarioAmigoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Usuario,ID_Amigo")] UsuarioAmigo usuarioAmigo)
        {
            if (id != usuarioAmigo.ID_Usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioAmigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioAmigoExists(usuarioAmigo.ID_Usuario))
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
            ViewData["ID_Amigo"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Amigo);
            ViewData["ID_Usuario"] = new SelectList(_context.Usuarios, "ID", "ID", usuarioAmigo.ID_Usuario);
            return View(usuarioAmigo);
        }

        // GET: UsuarioAmigoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioAmigo == null)
            {
                return NotFound();
            }

            var usuarioAmigo = await _context.UsuarioAmigo
                .Include(u => u.Amigo)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.ID_Usuario == id);
            if (usuarioAmigo == null)
            {
                return NotFound();
            }

            return View(usuarioAmigo);
        }

        // POST: UsuarioAmigoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioAmigo == null)
            {
                return Problem("Entity set 'Context.UsuarioAmigo'  is null.");
            }
            var usuarioAmigo = await _context.UsuarioAmigo.FindAsync(id);
            if (usuarioAmigo != null)
            {
                _context.UsuarioAmigo.Remove(usuarioAmigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioAmigoExists(int id)
        {
            return (_context.UsuarioAmigo?.Any(e => e.ID_Usuario == id)).GetValueOrDefault();
        }
    }*/
}
