namespace PixelPlay.Repositories.ReposInterface
{
    public interface IGameForm
    {
        public List<SelectListItem> GetDevicesData();

        public List<SelectListItem> GetCategoriesData();
    }
}
