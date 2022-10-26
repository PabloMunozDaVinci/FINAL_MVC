using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FINAL_MVC.Data;

namespace FINAL_MVC.Models
{
    /* public class TagPostsController : Controller
     {
         private readonly Context _context;

         public TagPostsController(Context context)
         {
             _context = context;
         }

         // GET: TagPosts
         public async Task<IActionResult> Index()
         {
             var context = _context.TagPost.Include(t => t.Post).Include(t => t.Tag);
             return View(await context.ToListAsync());
         }

         // GET: TagPosts/Details/5
         public async Task<IActionResult> Details(int? id)
         {
             if (id == null || _context.TagPost == null)
             {
                 return NotFound();
             }

             var tagPost = await _context.TagPost
                 .Include(t => t.Post)
                 .Include(t => t.Tag)
                 .FirstOrDefaultAsync(m => m.ID_Tag == id);
             if (tagPost == null)
             {
                 return NotFound();
             }

             return View(tagPost);
         }

         // GET: TagPosts/Create
         public IActionResult Create()
         {
             ViewData["ID_Post"] = new SelectList(_context.Posts, "ID", "ID");
             ViewData["ID_Tag"] = new SelectList(_context.Tags, "ID", "ID");
             return View();
         }

         // POST: TagPosts/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("ID_Tag,ID_Post,UltimaActualizacion")] TagPost tagPost)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(tagPost);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["ID_Post"] = new SelectList(_context.Posts, "ID", "ID", tagPost.ID_Post);
             ViewData["ID_Tag"] = new SelectList(_context.Tags, "ID", "ID", tagPost.ID_Tag);
             return View(tagPost);
         }

         // GET: TagPosts/Edit/5
         public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.TagPost == null)
             {
                 return NotFound();
             }

             var tagPost = await _context.TagPost.FindAsync(id);
             if (tagPost == null)
             {
                 return NotFound();
             }
             ViewData["ID_Post"] = new SelectList(_context.Posts, "ID", "ID", tagPost.ID_Post);
             ViewData["ID_Tag"] = new SelectList(_context.Tags, "ID", "ID", tagPost.ID_Tag);
             return View(tagPost);
         }

         // POST: TagPosts/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("ID_Tag,ID_Post,UltimaActualizacion")] TagPost tagPost)
         {
             if (id != tagPost.ID_Tag)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(tagPost);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!TagPostExists(tagPost.ID_Tag))
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
             ViewData["ID_Post"] = new SelectList(_context.Posts, "ID", "ID", tagPost.ID_Post);
             ViewData["ID_Tag"] = new SelectList(_context.Tags, "ID", "ID", tagPost.ID_Tag);
             return View(tagPost);
         }

         // GET: TagPosts/Delete/5
         public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.TagPost == null)
             {
                 return NotFound();
             }

             var tagPost = await _context.TagPost
                 .Include(t => t.Post)
                 .Include(t => t.Tag)
                 .FirstOrDefaultAsync(m => m.ID_Tag == id);
             if (tagPost == null)
             {
                 return NotFound();
             }

             return View(tagPost);
         }

         // POST: TagPosts/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.TagPost == null)
             {
                 return Problem("Entity set 'Context.TagPost'  is null.");
             }
             var tagPost = await _context.TagPost.FindAsync(id);
             if (tagPost != null)
             {
                 _context.TagPost.Remove(tagPost);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }

         private bool TagPostExists(int id)
         {
           return (_context.TagPost?.Any(e => e.ID_Tag == id)).GetValueOrDefault();
         }
     }*/
}
