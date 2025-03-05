namespace PixelPlay.Repositories.ReposInterface
{
    public interface IDevicesRepo
    {
        IEnumerable<SelectListItem> GetDevicesData();
    }
}
