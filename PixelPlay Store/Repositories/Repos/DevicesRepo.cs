using PixelPlay.Repositories.ReposInterface;

namespace PixelPlay.Repositories.Repos
{
    public class DevicesRepo : IDevicesRepo
    {
        private readonly MyDbContext context;
        public DevicesRepo(MyDbContext myDbContext)
        {
            context = myDbContext;
        }

        public IEnumerable<SelectListItem> GetDevicesData()
        {
            return context.Devices
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(d => d.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
