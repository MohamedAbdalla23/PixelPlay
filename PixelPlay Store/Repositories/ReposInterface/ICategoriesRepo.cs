namespace PixelPlay.Repositories.ReposInterface
{
    public interface ICategoriesRepo
    {
        IEnumerable<SelectListItem> GetCategoriesData();
    }
}
