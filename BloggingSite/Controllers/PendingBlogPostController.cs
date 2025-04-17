using BloggingSite.Models.ViewModel;
using BloggingSite.Services.IService;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite.Controllers
{
    [Authorize]
    public class PendingBlogPostController : Controller
    {
        private readonly IPendingBlogService _service;
        public PendingBlogPostController(IPendingBlogService service)
        {
            _service = service;
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
           _service.Approved(Obj);

           return RedirectToAction(nameof(Index));
        }
    }
}
