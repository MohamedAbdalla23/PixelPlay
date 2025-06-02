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
                .Take(4).ToList();

            //Get all Adventure Games separately
            var adventureGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 2))
                .Take(4).ToList();

            //Get all Arcade Games separately
            var arcadeGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 3))
                .Take(4).ToList();

            //Get all Horror Games separately
            var horrorGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 4))
                .Take(4).ToList();

            //Get all Horror Games separately
            var fightingGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 5))
                .Take(4).ToList();

            //Get all Horror Games separately
            var storyGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 6))
                .Take(4).ToList();

            //Get all Horror Games separately
            var shootingGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 7))
                .Take(4).ToList();

            //Get all Horror Games separately
            var sportGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 8))
                .Take(4).ToList();

            //Get all Horror Games separately
            var survivalGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 9))
                .Take(4).ToList();

            //Get all Horror Games separately
            var dramaGames = games
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 10))
                .Take(4).ToList();

            //Create a view model
            var viewModel = new GamesIndexViewModel
            {
                PagedGames = games,
                ActionGames = actionGames,
                AdventureGames = adventureGames,
                ArcadeGames = arcadeGames,
                HorrorGames = horrorGames,
                FightingGames = fightingGames,
                StoryGames = storyGames,
                ShootingGames = shootingGames,
                SportGames = sportGames,
                SurvivalGames = survivalGames,
                DramaGames = dramaGames
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
