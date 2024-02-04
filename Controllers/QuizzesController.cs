using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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

            var quizz = await _context.Quizzes
                .Include(q => q.tema)
                .Include(q => q.topico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            ViewBag.Tema = new SelectList(_context.Set<Tema>(), "Id", "nmtema");
            ViewBag.Topico = new SelectList(_context.Set<Topico>(), "Id", "nmtopico");
            return View();
        }

        public JsonResult GetTopicoByTemaId(int temaid)
        {
            return Json(_context.Topico.Where(q => q.temaid == temaid).ToList());
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,iduser,temaid,topicoid")] Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizz.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "nmtopico", quizz.topicoid);
            return View(quizz);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzes.FindAsync(id);
            if (quizz == null)
            {
                return NotFound();
            }
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizz.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "nmtopico", quizz.topicoid);
            return View(quizz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,iduser,temaid,topicoid")] Quizz quizz)
        {
            if (id != quizz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzExists(quizz.Id))
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
            ViewData["temaid"] = new SelectList(_context.Set<Tema>(), "Id", "nmtema", quizz.temaid);
            ViewData["topicoid"] = new SelectList(_context.Set<Topico>(), "Id", "nmtopico", quizz.topicoid);
            return View(quizz);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzes
                .Include(q => q.tema)
                .Include(q => q.topico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizz = await _context.Quizzes.FindAsync(id);
            if (quizz != null)
            {
                _context.Quizzes.Remove(quizz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
