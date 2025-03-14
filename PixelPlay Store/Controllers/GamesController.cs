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
        public IActionResult Index()
        {
            var games = gamesrepo.GetAll();
            return View("Index", games);
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
                //fix the drop down list style *****
            await gamesrepo.Create(model);
            await gamesrepo.Save();
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
