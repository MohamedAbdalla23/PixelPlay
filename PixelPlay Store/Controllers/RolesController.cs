using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PixelPlay.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> rolemanager;
        private readonly UserManager<ApplicationUser> usermanager;

        public RolesController( RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            rolemanager = roleManager;
            usermanager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {          
            return View(rolemanager.Roles);            
        }
       

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
               return View(ModelState);
            } 
            var role = new ApplicationRole()
            {
                Name = model.RoleName
            };
            var result = await rolemanager.CreateAsync(role);
            if (result.Succeeded==true)
            {
                ViewBag.success = true;
                return View(nameof(Index));
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return RedirectToAction(nameof(Index));            
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var role = await rolemanager.FindByIdAsync(id.ToString()!);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ApplicationRole model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            // Update properties
            role.Name = model.Name;

            var result = await rolemanager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // Show errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await rolemanager.FindByIdAsync(id.ToString()!);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            var result = await rolemanager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // Optional: handle delete failure
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Delete", role); // Show same view with errors
        }


        private async Task<bool> ApplicationRoleExists(int id)
        {
            var role = await rolemanager.FindByIdAsync(id.ToString());
            return role != null;
        }
    }
}
