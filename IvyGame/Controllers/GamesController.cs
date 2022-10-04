using IvyGame.Data;
using IvyGame.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IvyGame.Controllers
{
    public class GamesController : Controller
    {
        public GamesController(IvyGameContext context)
        {
            Context = context;
        }

        public IvyGameContext Context { get; }
        // GET: GamesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GamesController/Details/5
        [Route("Games/{id}")]
        public ActionResult Details(int id)
        {

            var game = Context.Game.FirstOrDefault(x => x.GameId == id);
            var gameDetails = Context.Game.Where(p => p.GameId == id).Take(1).ToList();
            // var gameDetails = Context.Game.FirstOrDefault(x => x.GameId == id).ToString();
            // var highScore = Context.HighScore.Include(p => p.Game).Where(p => p.UrlSlug == urlSlug).FirstOrDefault();
            // highScore = Context.HighScore.Where(x => x.GameId == id).OrderByDescending(x => x.Score).ToList();
            var sortByScore = Context.HighScore.Include(p => p.Game).Where(x => x.GameId == id).OrderByDescending(x => x.Score).ToList();

            // TODO Kontrollera att du plockar ut alla highscores kopplade till spelet
            /* var sortByScore = from s in sortScore
                               orderby s.Score descending
                               select s;*/
            // var sortByScore= from s in s
            var viewModel = new GameDetailsViewModel
            {
                Game = game,

                SortByScore = sortByScore,
                GameDetails = gameDetails,
            };

            return View(viewModel);
        }

        // GET: GamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GamesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GamesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
