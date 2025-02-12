using Microsoft.AspNetCore.Mvc;

namespace PixelPlay.Controllers
{
    public class GamesController : Controller
    {
        public GamesController() 
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View("Details", id);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Games games)
        {
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View("Update");
        }
        [HttpPost]
        public IActionResult Update(Games games)
        {
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View("Delete");
        }
        [HttpPost]
        public IActionResult Delete(Games games)
        {
            return RedirectToAction();
        }


    }
}
