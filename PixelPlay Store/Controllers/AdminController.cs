using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PixelPlay.Data;
using PixelPlay.Models;
using PixelPlay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlay.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly RoleManager<ApplicationRole> rolemanager;
        private readonly MyDbContext _context;

        public AdminController(MyDbContext context, SignInManager<ApplicationUser> signinManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            signinmanager = signinManager;
            usermanager = userManager;
            rolemanager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await usermanager.Users.ToListAsync();

            var userWithRoles = new List<UserWithRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await usermanager.GetRolesAsync(user);
                var mainRole = roles.FirstOrDefault() ?? "No Role";

                userWithRoles.Add(new UserWithRoleViewModel
                {      
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    Address = user.Address!,
                    Role = mainRole
                });
            }

            return View(userWithRoles);
        }
        

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await rolemanager.Roles.ToListAsync();

            var model = new CreateUserWithRoleViewModel
            {
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserWithRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateRolesAsync(model);
                return View(model);
            }

            var existingUser = await usermanager.FindByNameAsync(model.UserName)
                              ?? await usermanager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username or Email already exists.");
                await PopulateRolesAsync(model);
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.Phone
            };

            var result = await usermanager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                await PopulateRolesAsync(model);
                return View(model);
            }

            // Ensure role exists
            if (!await rolemanager.RoleExistsAsync(model.SelectedRole))
            {
                var createRoleResult = await rolemanager.CreateAsync(new ApplicationRole { Name = model.SelectedRole });
                if (!createRoleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Role creation failed.");
                    await PopulateRolesAsync(model);
                    return View(model);
                }
            }

            // Assign role
            var assignResult = await usermanager.AddToRoleAsync(user, model.SelectedRole);
            if (!assignResult.Succeeded)
            {
                foreach (var error in assignResult.Errors)
                    ModelState.AddModelError("", error.Description);

                await PopulateRolesAsync(model);
                return View(model);
            }

            return RedirectToAction("Index", "UserManagement");
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await usermanager.FindByIdAsync(id.Value.ToString());
            if (user == null)
                return NotFound();

            var currentRoles = await usermanager.GetRolesAsync(user);
            var allRoles = await rolemanager.Roles.ToListAsync();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                SelectedRole = currentRoles.FirstOrDefault() ?? "",
                Roles = allRoles.Select(role => new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate roles using role names
                model.Roles = rolemanager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();

                return View(model);
            }

            var user = await usermanager.FindByIdAsync(model.Id.ToString());
            if (user == null)
                return NotFound();

            // Update user information
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;

            var updateResult = await usermanager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                // Re-populate roles again before returning
                model.Roles = rolemanager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();

                return View(model);
            }

            // Get user's current role (only one role per user)
            var currentRoles = await usermanager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();

            // Find the role by exact name
            var matchedRole = await rolemanager.FindByNameAsync(model.SelectedRole);
            if (matchedRole == null)
            {
                ModelState.AddModelError("SelectedRole", $"Role '{model.SelectedRole}' does not exist.");
                model.Roles = rolemanager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();

                return View(model);
            }

            // If role changed, update
            if (currentRole != matchedRole.Name)
            {
                if (currentRole != null)
                    await usermanager.RemoveFromRoleAsync(user, currentRole);

                await usermanager.AddToRoleAsync(user, matchedRole.Name);
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await rolemanager.FindByIdAsync(id.ToString()!);
            if (role == null)
                return NotFound();

            return View(role);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await rolemanager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            var result = await rolemanager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(role); // return with errors
            }

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult Dashboard()
        //{
        //    //if (User.Identity!.IsAuthenticated)
        //    //{
        //    //    return View();
        //    //}
        //    //return Unauthorized();
        //    return View();
        //}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = await usermanager.Users.CountAsync(),
                TotalRoles = await rolemanager.Roles.CountAsync(),
                RecentUsers = await usermanager.Users
                    .OrderByDescending(u => u.Id) // or CreatedAt if available
                    .Take(5)
                    .ToListAsync()
            };

            return View(model);
        }


        private async Task PopulateRolesAsync(CreateUserWithRoleViewModel model)
        {
            model.Roles = (await rolemanager.Roles.ToListAsync())
                .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
                .ToList();
        }
        


    }
}
