using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizzApp.Areas.Identity.Data;
using QuizzApp.Models;

namespace QuizzApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly ApplicationDBcontext _context;

        public QuizzesController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            var applicationDBcontext = _context.Quizzes.Include(q => q.tema).Include(q => q.topico);
            return View(await applicationDBcontext.ToListAsync());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzes = await _context.Quizzes
                .Include(q => q.tema)
                .Include(q => q.topico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzes == null)
            {
                return NotFound();
            }

            return View(quizzes);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema");
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "Id");
            return View();
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,iduser,temaid,topicoid")] Quizzes quizzes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizzes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizzes.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "Id", quizzes.topicoid);
            return View(quizzes);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzes = await _context.Quizzes.FindAsync(id);
            if (quizzes == null)
            {
                return NotFound();
            }
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizzes.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "Id", quizzes.topicoid);
            return View(quizzes);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,iduser,temaid,topicoid")] Quizzes quizzes)
        {
            if (id != quizzes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizzes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzesExists(quizzes.Id))
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
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizzes.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "Id", quizzes.topicoid);
            return View(quizzes);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzes = await _context.Quizzes
                .Include(q => q.tema)
                .Include(q => q.topico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzes == null)
            {
                return NotFound();
            }

            return View(quizzes);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizzes = await _context.Quizzes.FindAsync(id);
            if (quizzes != null)
            {
                _context.Quizzes.Remove(quizzes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzesExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
