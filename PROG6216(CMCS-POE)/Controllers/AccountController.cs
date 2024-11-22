using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG6216_CMCS_POE_.Models;

namespace PROG6216_CMCS_POE_.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Ensure role exists
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Error creating role.");
                        return View(model);
                    }
                }

                // Assign role to user
                var roleAssignResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (roleAssignResult.Succeeded)
                {
                    return RedirectToAction("Login");

                }

                // Log errors during role assignment
                foreach (var error in roleAssignResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                // Log creation errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UsernameOrEmail)
                           ?? await _userManager.FindByEmailAsync(model.UsernameOrEmail);

                if (user != null)
                {
                    if (!await _userManager.IsLockedOutAsync(user))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account is locked.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or email.");
                }
            }

            return View(model);
        }

        // Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Access Denied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
