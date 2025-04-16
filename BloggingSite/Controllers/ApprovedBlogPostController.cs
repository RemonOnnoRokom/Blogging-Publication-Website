using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Mvc;

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
            var list = _context.PendingBlogs.Where(x => x.Id == id).FirstOrDefault();
            _context.PendingBlogs.Remove(list);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
