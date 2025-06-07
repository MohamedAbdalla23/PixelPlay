using Microsoft.AspNetCore.Mvc;
using PixelPlay.Models;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IGameRepo gamesrepo;
        private readonly ICategoriesRepo categoryrepo;

        public CategoriesController(MyDbContext context, IGameRepo gameRepo, ICategoriesRepo categoriesRepo)
        {
            _context = context;
            gamesrepo = gameRepo;
            categoryrepo = categoriesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var categories = await categoryrepo.GetCategory()
                .Include(x=>x.GameCategories).ToListAsync();            

            var viewmodel = categories.Select(cat => new CategoryGameCountViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                NoGameCount = cat.GameCategories.Count
            }).ToList();

            return View(viewmodel);
        }

      
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Categories categories)
        {
            if (ModelState.IsValid)
            {               
                await categoryrepo.AddCategory(categories);
                await categoryrepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var categories = await categoryrepo.GetCategorybyId(id);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(categories);
                    await categoryrepo.UpdateCategory(categories);
                    await categoryrepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }


        [HttpDelete]         
        public IActionResult Delete(int id)
        {
            var isDeleted = categoryrepo.Delete(id);

            if (isDeleted)
            {
                return Ok(new { message = "Category deleted successfully." });
            }

            return NotFound(new { message = "Category not found." });
        }


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var categories = await _context.Categories.FindAsync(id);
        //    if (categories != null)
        //    {
        //        _context.Categories.Remove(categories);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool CategoriesExists(int id)
        {
            //return _context.Categories.Any(e => e.Id == id);
            return categoryrepo.Categoryisexist(id);
        }
    }
}
