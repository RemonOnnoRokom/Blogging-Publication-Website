using System.ComponentModel.DataAnnotations;
using BloggingSite.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole<long>> _roleManager;
        private UserManager<MyUser> _userManager;

        public RoleController(RoleManager<IdentityRole<long>> roleManger , UserManager<MyUser> userManager)
        {
            _roleManager = roleManger;
            _userManager = userManager;
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        public IActionResult Index()
        {
            var list = _roleManager.Roles.ToList();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole<long>(name));
                if (result.Succeeded)
                {
                    MyUser user = new MyUser()
                    {
                        Name = "Sys",
                        UserName = "SYS",
                        Email = "sys@gmail.com",

                    };

                    var res = await _userManager.CreateAsync(user, "123456");
                    
                    if (res.Succeeded)
                    {
                        var userRole = await _userManager.FindByNameAsync("Sys");
                        if (userRole != null)
                        {
                            await _userManager.AddToRoleAsync(userRole, name);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                        Errors(res);                    
                }
                    
                else
                    Errors(result);
            }
            return View(name);
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole<long> role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", _roleManager.Roles);
        }
    }
}
