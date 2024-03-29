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
    public class PerguntasController : Controller
    {
        private readonly ApplicationDBcontext _context;

        public PerguntasController(ApplicationDBcontext context)
        {
            _context = context;
        }

        // GET: Perguntas
        public async Task<IActionResult> Index(int? quizzId)
        {
            if (quizzId == null)
            {
                return NotFound();
            }

            ViewBag.QuizzId = quizzId;

            var perguntas = _context.Pergunta.Where(p => p.quizzid == quizzId);
            return View(await perguntas.ToListAsync());
        }

        // GET: Perguntas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Pergunta
                .Include(p => p.quizz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pergunta == null)
            {
                return NotFound();
            }

            return View(pergunta);
        }

        // GET: Perguntas/Create
        public IActionResult Create(int? quizzid)
        {
            if (quizzid == null)
            {
                return NotFound();
            }
            ViewBag.QuizzId = quizzid;
            ViewData["quizzid"] = new SelectList(_context.Quizzes, "Id", "Descricao");
            return View(new Pergunta { quizzid = quizzid.Value });
        }

        // POST: Perguntas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,Resposta1,Resposta2,Resposta3,Resposta4,RespostaCorreta,quizzid")] Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pergunta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { quizzId = pergunta.quizzid });
            }
            ViewData["quizzid"] = new SelectList(_context.Quizzes, "Id", "Descricao", pergunta.quizzid);
            return View(pergunta);
        }

        // GET: Perguntas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Pergunta.FindAsync(id);
            if (pergunta == null)
            {
                return NotFound();
            }
            ViewData["quizzid"] = new SelectList(_context.Quizzes, "Id", "Descricao", pergunta.quizzid);
            return View(pergunta);
        }

        // POST: Perguntas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto,Resposta1,Resposta2,Resposta3,Resposta4,RespostaCorreta,quizzid")] Pergunta pergunta)
        {
            if (id != pergunta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pergunta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerguntaExists(pergunta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirecionar para o Index de Perguntas mantendo o quizzId
                return RedirectToAction("Index", new { quizzId = pergunta.quizzid });
            }
            ViewData["quizzid"] = new SelectList(_context.Quizzes, "Id", "Descricao", pergunta.quizzid);
            return View(pergunta);
        }

        // GET: Perguntas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pergunta = await _context.Pergunta
                .Include(p => p.quizz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pergunta == null)
            {
                return NotFound();
            }

            return View(pergunta);
        }

        // POST: Perguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pergunta = await _context.Pergunta.FindAsync(id);
            if (pergunta != null)
            {
                int quizzId = pergunta.quizzid;
                _context.Pergunta.Remove(pergunta);
                await _context.SaveChangesAsync();

                // Redirecionar para o Index de Perguntas mantendo o quizzId
                return RedirectToAction("Index", new { quizzId });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PerguntaExists(int id)
        {
            return _context.Pergunta.Any(e => e.Id == id);
        }
    }
}