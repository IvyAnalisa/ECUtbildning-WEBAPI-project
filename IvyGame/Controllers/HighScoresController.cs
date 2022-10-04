#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IvyGame.Data;
using IvyGame.Models.Domain;

namespace IvyGame.Controllers
{
    public class HighScoresController : Controller
    {
        private readonly IvyGameContext _context;

        public HighScoresController(IvyGameContext context)
        {
            _context = context;
        }

        // GET: HighScores
        public async Task<IActionResult> Index()
        {
            var ivyGameContext = _context.HighScore.Include(h => h.Game);
            return View(await ivyGameContext.ToListAsync());
        }

        // GET: HighScores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highScore = await _context.HighScore
                .Include(h => h.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highScore == null)
            {
                return NotFound();
            }

            return View(highScore);
        }

        // GET: HighScores/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameName");
            return View();
        }

        // POST: HighScores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Date,Player,Game,GameId")] HighScore highScore)
        {// för att funka Data skicka till backend. ModeState.IsValid måste vara TRUE och Alla varder blie Valid
            // sätta Nullable is Game Game i Highscore = nulllable
            if (ModelState.IsValid)
            {
                _context.Add(highScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameName", highScore.GameId);
            return View(highScore);
        }

        // GET: HighScores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highScore = await _context.HighScore.FindAsync(id);
            if (highScore == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameName", highScore.GameId);
            return View(highScore);
        }

        // POST: HighScores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Date,Player,Game,GameId")] HighScore highScore)
        {
            if (id != highScore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(highScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HighScoreExists(highScore.Id))
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
            ViewData["GameId"] = new SelectList(_context.Game, "GameId", "GameName", highScore.GameId);
            return View(highScore);
        }

        // GET: HighScores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var highScore = await _context.HighScore
                .Include(h => h.Game)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (highScore == null)
            {
                return NotFound();
            }

            return View(highScore);
        }

        // POST: HighScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var highScore = await _context.HighScore.FindAsync(id);
            _context.HighScore.Remove(highScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HighScoreExists(int id)
        {
            return _context.HighScore.Any(e => e.Id == id);
        }
    }
}
