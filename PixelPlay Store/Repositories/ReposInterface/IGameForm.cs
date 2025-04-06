namespace PixelPlay.Repositories.ReposInterface
{
    public interface IGameForm
    {
        //The Old Interface(Abstraction)
        //************************************
        //public List<SelectListItem> GetDevicesData();

        //public List<SelectListItem> GetCategoriesData();

        public Task<string> SaveCover(IFormFile file);
    }
}
