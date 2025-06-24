using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PixelPlay.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly UserManager<ApplicationUser> usermanager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            usermanager = userManager;
            signinmanager = signInManager;           
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterFormViewModel model)
        {
            //Error
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {               
                UserName = model.UserName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.Phone               
            };

            //save to db
            var result = await usermanager.CreateAsync(user, model.Password!);
            
            //cookie
            if (result.Succeeded)
            {
                await signinmanager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
               
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginFormViewModel model)
        {
            //Error
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //check found
            var user = await usermanager.FindByNameAsync(model.LoginInput!) 
                    ?? await usermanager.FindByEmailAsync(model.LoginInput!);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User cannot be found");
                return View(model);
            }
            var found = await signinmanager.CheckPasswordSignInAsync(user, model.Password!, false);

            //create cookie
            if (found.Succeeded)
            {
                await signinmanager.SignInAsync(user, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
            return View();
        }

        
        public async Task<ActionResult> Logout()
        {
            await signinmanager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
