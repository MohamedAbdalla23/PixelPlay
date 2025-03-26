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
            
            int pageSize = 3;
            var games = gamesrepo.GetAll();
            var paginatedGames = await PaginatedList<Games>.CreateAsync(games, pageNumber ?? 1, pageSize);
            return View(paginatedGames);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Games games = gamesrepo.GetById(id);
            return View("Details", games);
        }


        [HttpGet]
        public IActionResult Create()
        {
            GameFormViewModel Viewmodel = new()
            {
                Categories = categoriesrepo.GetCategoriesData(),
                Devices = devicesrepo.GetDevicesData()
            };
            return View(Viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            } //the drop down list don't show when submit an invalid game *****
                
            await gamesrepo.Create(model);
            await gamesrepo.Save();
            TempData["SuccessMessage"] = "Game created successfully!";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            Games games = gamesrepo.GetById(id);
            return View("Update", games);
        }
        [HttpPost]
        public IActionResult Update(Games games)
        {
            gamesrepo.Update(games);
            gamesrepo.Save();
            return RedirectToAction("Details");
        }


        [HttpGet]
        public IActionResult Delete()
        {
            return View("Delete");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            gamesrepo.Delete(id);
            gamesrepo.Save();
            return RedirectToAction();
        }


    }
}
