namespace PixelPlay.Repositories.ReposInterface
{
    public interface IDevicesRepo
    {
        public IEnumerable<SelectListItem> GetDevicesData();

        public IQueryable<Devices> GetDevice();

        public Task<Devices> GetDevicebyId(int? id);

        public Task AddDevice(Devices Devices);

        public Task UpdateDevice(Devices Devices);

        public bool Delete(int id);

        public Task Save();

        public bool DeviceIsExist(int id);
    }
}
