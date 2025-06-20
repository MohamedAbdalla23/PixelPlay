using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoriesRepo categoryrepo;

        public AdminCategoriesController(ICategoriesRepo categoriesRepo)
        {
            categoryrepo = categoriesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await categoryrepo.GetCategory()
                .Include(x => x.GameCategories).ToListAsync();

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

        private bool CategoriesExists(int id)
        {
            return categoryrepo.CategoryIsExist(id);
        }
    }
}
