using Microsoft.AspNetCore.Mvc;
using PixelPlay.Repositories.ReposInterface;


namespace PixelPlay.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDevicesRepo devicerepo;

        public DevicesController(IDevicesRepo devicesRepo)
        {
            devicerepo = devicesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await devicerepo.GetDevice()
                .Include(x => x.GameDevices).ToListAsync();

            var viewmodel = categories.Select(cat => new DeviceGameCountViewModel
            {
                Id = cat.Id,
                Icon = cat.Icon,
                Name = cat.Name,
                NoGameCount = cat.GameDevices.Count
            }).ToList();

            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Icon,Id,Name")] Devices devices)
        {
            if (ModelState.IsValid)
            {
                await devicerepo.AddDevice(devices);
                await devicerepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(devices);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devices = await devicerepo.GetDevicebyId(id);
            if (devices == null)
            {
                return NotFound();
            }
            return View(devices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Icon,Id,Name")] Devices devices)
        {
            if (id != devices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await devicerepo.UpdateDevice(devices);
                    await devicerepo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevicesExists(devices.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(devices);
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = devicerepo.Delete(id);

            if (isDeleted)
            {
                return Ok(new { message = "Category deleted successfully." });
            }

            return NotFound(new { message = "Category not found." });
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var devices = await _context.Devices
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (devices == null)
            //{
            //    return NotFound();
            //}

            //return View(devices);
        }


        private bool DevicesExists(int id)
        {
            return devicerepo.DeviceIsExist(id);
        }

    }
}
