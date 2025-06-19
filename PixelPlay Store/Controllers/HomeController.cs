using Microsoft.AspNetCore.Mvc;
using PixelPlay_Store.Models;
using System.Diagnostics;

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
            var games = await gamesrepo.GetAll().Take(8).ToListAsync();

            var game = await gamesrepo.GetAll().ToListAsync();

            //Get all Action Games separately
            var actionGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 1))
                .Take(6).ToList();

            //Get all Adventure Games separately
            var adventureGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 2))
                .Take(6).ToList();

            //Get all Arcade Games separately
            var arcadeGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 3))
                .Take(6).ToList();

            //Get all Horror Games separately
            var horrorGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 4))
                .Take(6).ToList();

            //Get all Fighting Games separately
            var fightingGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 5))
                .Take(6).ToList();

            //Get all Story Games separately
            var storyGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 6))
                .Take(6).ToList();

            //Get all Shooting Games separately
            var shootingGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 7))
                .Take(6).ToList();

            //Get all Sport Games separately
            var sportGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 8))
                .Take(6).ToList();

            //Get all Survival Games separately
            var survivalGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 9))
                .Take(6).ToList();

            //Get all Drama Games separately
            var dramaGames = game
                .Where(g => g.GameCategories.Any(gc => gc.CategoryId == 10))
                .Take(6).ToList();

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
