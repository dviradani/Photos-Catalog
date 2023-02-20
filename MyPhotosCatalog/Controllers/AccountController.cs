using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPhotosCatalog.Models.ViewModels;

namespace MyPhotosCatalog.Controllers
{
    //Responsible for user management
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //First user registration , (for now..) , Later it will be used for normal user registration
        public async Task<IActionResult> Register()
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "dvirad",
            };

            var result = await _userManager.CreateAsync(user, "123qwe!@#QWE");

            return RedirectToAction("HomePage", "User");
        }
        [HttpGet]
        [Route("Identity/Account/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Identity/Account/Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Username!, loginModel.Password!, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Catalog", "Admin");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "User");
        }
    }
}
