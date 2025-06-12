using PixelPlay.Models;
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

        public IQueryable<Devices> GetDevice()
        {
            return context.Devices;
        }

        public Task<Devices> GetDevicebyId(int? id)
        {
            return context.Devices.FirstOrDefaultAsync(c => c.Id == id)!;
        }

        public async Task AddDevice(Devices Devices)
        {
            await context.Devices.AddAsync(Devices);
        }

        public async Task UpdateDevice(Devices Devices)
        {
            context.Devices.Update(Devices);
            await Save();
        }

        public bool Delete(int id)
        {
            try
            {
                var devices = context.Devices.Find(id);
                if (devices == null)
                    return false;

                context.Devices.Remove(devices);
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

        public bool DeviceIsExist(int id)
        {
            return context.Devices.Any(e => e.Id == id);
        }                         
    }
}
