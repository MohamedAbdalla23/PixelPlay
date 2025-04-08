using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            int pageSize = 6;
            var games = gamesrepo.GetAll();
            var paginatedGames = await PaginatedList<Games>.CreateAsync(games, pageNumber ?? 1, pageSize);
            return View(paginatedGames);
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
