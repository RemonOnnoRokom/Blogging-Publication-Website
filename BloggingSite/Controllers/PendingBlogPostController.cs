using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BloggingSite.Services.IService;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PendingBlogPostController : Controller
    {
        private readonly IPendingBlogService _service;
        private UserManager<MyUser> _userManager;
        public PendingBlogPostController(IPendingBlogService service , UserManager<MyUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();

            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
             await _service.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Approved(int id)
        {
            var obj = await _service.GetByIdAsync(id);

            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Approved(PendingBlog Obj)
        {
            long id = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
           _service.Approved(Obj,id);

           return RedirectToAction(nameof(Index));
        }
    }
}
