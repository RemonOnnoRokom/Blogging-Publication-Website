using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    [Authorize]
    public class MyBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;
        public MyBlogsController(ApplicationDbContext context , UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ApprovedBlogVM approvedBlogVM = new ApprovedBlogVM();
            long id = ( await _userManager.FindByNameAsync(User.Identity.Name)).Id;

            approvedBlogVM.ApprovedBlogs = _context.ApprovedBlogs.Where(x=>x.CreatedBy == id).Take(6).ToList();
            approvedBlogVM.ItemNumber = 5;

            return View(approvedBlogVM);
        }

        public IActionResult LoadMore(int skip)
        {


            return View();
        }
    }
}
