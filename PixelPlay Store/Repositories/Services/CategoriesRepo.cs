using Microsoft.EntityFrameworkCore;
using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class CategoriesRepo : ICategoriesRepo
    {
        private readonly MyDbContext context;
        public CategoriesRepo(MyDbContext myDbContext)
        {
            context = myDbContext; 
        }

        public IEnumerable<SelectListItem> GetCategoriesData()
        {
            return context.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }

        public IQueryable<Categories> GetCategory()
        {
            return context.Categories;
        }

        public Task<Categories> GetCategorybyId(int? id)
        {
            return context.Categories.FirstOrDefaultAsync(c => c.Id == id)!;
        }

        public async Task AddCategory(Categories categories)
        {
            await context.Categories.AddAsync(categories);          
        }

        public async Task UpdateCategory(Categories categories)
        {
            context.Categories.Update(categories); 
            await Save();      
        }

        public bool Delete(int id)
        {
            try
            {
                var category = context.Categories.Find(id);
                if (category == null)
                    return false;

                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            catch
            {
                // Log the exception if needed
                return false;
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public bool Categoryisexist(int id)
        {
           return context.Categories.Any(e => e.Id == id);
        }
    }
}
