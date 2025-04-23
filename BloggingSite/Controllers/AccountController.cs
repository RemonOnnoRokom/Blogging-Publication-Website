using BloggingSite.Models.Entities;
using BloggingSite.Models.LoginRegistration;
using BloggingSite.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        public AccountController(SignInManager<MyUser> signInManager, UserManager<MyUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Name, model.password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            return View(model);
        }

        
        public async Task<IActionResult> LogOut()
        {
            //await base.OnInitializedAsync();
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                MyUser user = new MyUser()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,

                };

                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");


                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM obj)
        {
            var person = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _userManager.ChangePasswordAsync(person, obj.CurrentPassword, obj.NewPassword);
            if (result.Result.Succeeded)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(obj);
        }
    }
}
