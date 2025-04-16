using BloggingSite.Models.Entities;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite.Controllers
{
    public class ApprovedBlogPostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ApprovedBlogPostController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.PendingBlogs.ToList();

            return View(list);
        }

        public IActionResult Delete(int id)
        {
            var obj = _context.PendingBlogs.Where(x => x.Id == id).FirstOrDefault();
            _context.PendingBlogs.Remove(obj);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Approved(int id)
        {
            var obj = _context.PendingBlogs.FirstOrDefault(x => x.Id == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult Approved(PendingBlog obj)
        {
            ApprovedBlog ApprovedObj = new ApprovedBlog();
            ApprovedObj.Content = obj.Content.Substring(0);
            ApprovedObj.CreatedDate = obj.CreatedDate;

            ApprovedObj.CreatedBy = 2;
            ApprovedObj.PublishedDate = DateTime.Now;

            _context.ApprovedBlogs.Add(ApprovedObj);
            _context.PendingBlogs.Remove(obj);

           return RedirectToAction(nameof(Index));
        }
    }
}
