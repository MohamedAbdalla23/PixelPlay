using Microsoft.AspNetCore.Mvc;

namespace PixelPlay.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        private readonly DbContext _dbContext;
        public OrderController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //[HttpGet]
        //public async Task< IActionResult> Checkout()
        //{
        //    var order = new Orders();
        //    return View(order);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Checkout()
        //{
        //    var order = new Orders();
        //    return View(order);
        //}
    }
}
