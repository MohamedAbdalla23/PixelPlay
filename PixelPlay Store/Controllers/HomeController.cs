using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;
using PixelPlay_Store.Models;
using System.Diagnostics;

namespace PixelPlay_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameRepo gamesrepo;
        private readonly ICategoriesRepo categoriesrepo;

        public HomeController(ILogger<HomeController> logger, IGameRepo gamesRepo, ICategoriesRepo categoriesRepo)
        {
            gamesrepo = gamesRepo;
            _logger = logger;
            categoriesrepo = categoriesRepo;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {           
            var games = await gamesrepo.GetAll().ToListAsync();

            return View(games);
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
