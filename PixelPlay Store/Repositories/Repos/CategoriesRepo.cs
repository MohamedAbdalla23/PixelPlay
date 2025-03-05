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
    }
}
