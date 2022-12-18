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
    public class ReaccionsController : Controller
    {
        private readonly Context _context;

        public ReaccionsController(Context context)
        {
            _context = context;
        }

        // GET: Reaccions
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            var context = _context.Reacciones.Include(r => r.Post).Include(r => r.Usuario);
            return View(await context.ToListAsync());
        }

        // GET: Reaccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };
            if (id == null || _context.Reacciones == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones
                .Include(r => r.Post)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reaccion == null)
            {
                return NotFound();
            }

            return View(reaccion);
        }

        // GET: Reaccions/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID");
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID");
            return View();
        }

        // POST: Reaccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Tipo,PostID,UsuarioID")] Reaccion reaccion)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            if (ModelState.IsValid)
            {
                _context.Add(reaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reaccion.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", reaccion.UsuarioID);
            return View(reaccion);
        }

        // GET: Reaccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            if (id == null || _context.Reacciones == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones.FindAsync(id);
            if (reaccion == null)
            {
                return NotFound();
            }
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reaccion.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", reaccion.UsuarioID);
            return View(reaccion);
        }

        // POST: Reaccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Tipo,PostID,UsuarioID")] Reaccion reaccion)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");

            };
            if (id != reaccion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaccionExists(reaccion.ID))
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
            ViewData["PostID"] = new SelectList(_context.Posts, "ID", "ID", reaccion.PostID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "ID", "ID", reaccion.UsuarioID);
            return View(reaccion);
        }

        // GET: Reaccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            if (id == null || _context.Reacciones == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones
                .Include(r => r.Post)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reaccion == null)
            {
                return NotFound();
            }

            return View(reaccion);
        }

        // POST: Reaccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UsuarioLogueado") == null)
            {
                return RedirectToAction("Index", "Home");
            };

            if (_context.Reacciones == null)
            {
                return Problem("Entity set 'Context.Reacciones'  is null.");
            }
            var reaccion = await _context.Reacciones.FindAsync(id);
            if (reaccion != null)
            {
                _context.Reacciones.Remove(reaccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReaccionExists(int id)
        {
            return (_context.Reacciones?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
