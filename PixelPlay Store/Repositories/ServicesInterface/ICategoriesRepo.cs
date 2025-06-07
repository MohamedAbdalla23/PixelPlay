using Microsoft.EntityFrameworkCore;

namespace PixelPlay.Repositories.ReposInterface
{
    public interface ICategoriesRepo
    {
        public IEnumerable<SelectListItem> GetCategoriesData();

        public IQueryable<Categories> GetCategory();

        public Task<Categories> GetCategorybyId(int? id);

        public Task AddCategory(Categories categories);

        public Task UpdateCategory(Categories categories);

        public bool Delete(int id);

        public Task Save();

        public bool Categoryisexist(int id);
    }
}
