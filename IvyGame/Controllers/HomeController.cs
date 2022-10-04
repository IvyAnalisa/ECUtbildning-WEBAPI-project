using IvyGame.Data;
using IvyGame.Models;
using IvyGame.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IvyGame.Controllers
{
    public class HomeController : Controller
    {
        public IvyGameContext Context { get; }
        public HomeController(IvyGameContext context)
        {
            Context = context;
        }

        /* private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            var highScores = Context.HighScore.Include(p => p.Game).ToList();
            // var game = Context.Game.ToList();

            /*var displayOnHomePage = from s in highScores
                                    orderby s.Score descending
                                    select s;*/

            var displayOnHomePage = highScores.OrderByDescending(s => s.Score);




            var viewModel = new HomeIndexViewModel
            {

                //Dipslay Top Pick Movie in Index page
                DisplayOnHomePage = displayOnHomePage,
                //SortByScore = sortByScore,
            };
            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}