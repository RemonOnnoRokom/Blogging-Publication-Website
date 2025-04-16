using BloggingSite.Models.Entities;
using BloggingSite.Services.IService;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite.Controllers
{
    public class ApprovedBlogPostController : Controller
    {
        private readonly IApprovedBlogService _service;
        public ApprovedBlogPostController(IApprovedBlogService service)
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
        public IActionResult Approved(PendingBlog obj)
        {
            //ApprovedBlog ApprovedObj = new ApprovedBlog();
            //ApprovedObj.Content = obj.Content.Substring(0);
            //ApprovedObj.CreatedDate = obj.CreatedDate;

            //ApprovedObj.CreatedBy = 2;
            //ApprovedObj.PublishedDate = DateTime.Now;

            //_service.ApprovedBlogs.Add(ApprovedObj);
            //_service.PendingBlogs.Remove(obj);

           return RedirectToAction(nameof(Index));
        }
    }
}
