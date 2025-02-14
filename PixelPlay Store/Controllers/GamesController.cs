using Microsoft.AspNetCore.Mvc;

namespace PixelPlay.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameRepo gamesrepo;
        public GamesController(IGameRepo gameRepo) 
        {
            gamesrepo = gameRepo;
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
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Games games)
        {
            gamesrepo.Create(games);
            gamesrepo.Save();
            return RedirectToAction();
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
