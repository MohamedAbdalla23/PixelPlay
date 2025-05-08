using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.Repos;
using PixelPlay_Store.Models;

namespace PixelPlay_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepo gamesrepo;

        public HomeController(ILogger<HomeController> logger, IGameRepo gamesRepo)
        {
            gamesrepo = gamesRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var games = gamesrepo.GetAll().Take(8).ToList();            

            //Get all Action Games separately
            var actionGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 1))
                .Take(6).ToList();

            //Create a view model
            var viewModel = new GamesIndexViewModel
            {
                PagedGames = games,
                ActionGames = actionGames
            };

            return View(viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
