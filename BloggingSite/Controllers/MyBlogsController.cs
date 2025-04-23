using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Index(int skip = 0 )
        {
            ApprovedBlogVM approvedBlogVM = new ApprovedBlogVM();

            long id = ( await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var searcherObj = _context.ApprovedBlogs.Where(x => x.CreatedBy == id);

            if (skip > 0)
            {
                searcherObj = searcherObj.Skip(skip - 5 );
            }

            approvedBlogVM.ApprovedBlogs = searcherObj.Take(6).ToList();
            approvedBlogVM.ItemNumber += skip;

            return View(approvedBlogVM);
        }

        public async Task<IActionResult> Search(BlogStatus search ,int skip)
        {
            if(search == BlogStatus.None)
            {
                return RedirectToAction(nameof(Index));
            }
            ApprovedBlogVM approvedBlogVM = new ApprovedBlogVM();

            long id = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
            var obj = _context.ApprovedBlogs.Where(x => x.CreatedBy == id && x.CurrentStatus == search);

            if (skip > 0)
            {
                obj = obj.Skip(skip - 5);
            }

            approvedBlogVM.ApprovedBlogs = obj.Take(6).ToList();            
            approvedBlogVM.ItemNumber += skip;

            return View(nameof(Index),approvedBlogVM);
        }
    }
}
