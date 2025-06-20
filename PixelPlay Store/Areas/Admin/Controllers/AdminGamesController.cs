using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGamesController : Controller
    {
        private readonly IGameRepo gamesrepo;
        private readonly IGameForm gameformrepo;
        private readonly ICategoriesRepo categoriesrepo;
        private readonly IDevicesRepo devicesrepo;
        public AdminGamesController(IGameRepo gameRepo, IGameForm gameForm, ICategoriesRepo categoriesRepo, IDevicesRepo devicesRepo)
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



        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel Viewmodel = new()
            {
                Categories = categoriesrepo.GetCategoriesData(),
                Devices = devicesrepo.GetDevicesData()
            };
            return View(Viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Populate the model with devices and categories for the view
                model.Devices = devicesrepo.GetDevicesData();
                model.Categories = categoriesrepo.GetCategoriesData();
                return Ok(new { success = false, message = "Invalid data", model });
            }

            // Proceed with saving the game if valid
            await gamesrepo.Create(model);
            await gamesrepo.Save();

            // Return success status and message for AJAX response
            return Ok(new { success = true, message = "Game created successfully!" });
        }



        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = gamesrepo.GetById(id);
            if (game is null)
            {
                return NotFound();
            }
            EditGameFormViewModel model = new()
            {
                Id = id,
                Name = game.Name,
                Price = game.Price,
                Trailer = game.Trailer,
                Description = game.Description,
                GameDevices = game.GameDevices.Select(d => d.DeviceId).ToList(),
                GameCategories = game.GameCategories.Select(c => c.CategoryId).ToList(),
                Devices = devicesrepo.GetDevicesData(),
                Categories = categoriesrepo.GetCategoriesData(),
                CurrentCover = game.Cover
            };
            return View("Update", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Devices = devicesrepo.GetDevicesData();
                model.Categories = categoriesrepo.GetCategoriesData();
                return View(model);
            }

            var game = await gamesrepo.UpdateGameAsync(model);
            if (game is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = gamesrepo.Delete(id);

            return isDeleted
                ? Ok(new { message = "Game deleted successfully." })
                : NotFound(new { message = "Game not found." });
        }

        //[HttpPost]
        //public IActionResult Delete()
        //{

        //    gamesrepo.Save();
        //    return RedirectToAction();
        //}


    }
}
