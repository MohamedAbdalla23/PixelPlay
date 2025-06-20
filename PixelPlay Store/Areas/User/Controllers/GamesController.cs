using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Areas.User.Controllers
{
    [Area("User")]
    public class GamesController : Controller
    {
        private readonly IGameRepo gamesrepo;
        private readonly IGameForm gameformrepo;
        private readonly ICategoriesRepo categoriesrepo;
        private readonly IDevicesRepo devicesrepo;
        public GamesController(IGameRepo gameRepo, IGameForm gameForm, ICategoriesRepo categoriesRepo, IDevicesRepo devicesRepo)
        {
            gamesrepo = gameRepo;
            gameformrepo = gameForm;
            categoriesrepo = categoriesRepo;
            devicesrepo = devicesRepo;
        }

        public async Task<IActionResult> Index(string sortOrder, string category, string device, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SelectedCategory"] = category;
            ViewData["SelectedDevice"] = device;
            ViewData["CurrentFilter"] = searchString;

            var games = gamesrepo.GetAll(); // IQueryable<Games>

            if (!string.IsNullOrEmpty(category))
            {
                games = games.Where(g => g.GameCategories.Any(gc => gc.CategoryId.ToString() == category));
            }

            if (!string.IsNullOrEmpty(device))
            {
                games = games.Where(g => g.GameDevices.Any(gd => gd.DeviceId.ToString() == device));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Name.Contains(searchString));
            }

            games = sortOrder switch
            {
                "name_desc" => games.OrderByDescending(g => g.Name),
                "price_asc" => games.OrderBy(g => g.Price),
                "price_desc" => games.OrderByDescending(g => g.Price),
                _ => games.OrderBy(g => g.Name),
            };

            return View(await games.ToListAsync());
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            var games = gamesrepo.GetById(id);
            if (games is null)
            {
                return NotFound();
            }
            return View("Details", games);
        }        
       
    }
}
