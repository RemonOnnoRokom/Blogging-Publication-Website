using System.Diagnostics;
using BloggingSite.Models;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,UserManager<MyUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager; 
        }

        public IActionResult Index()
        {
            var list = _context.ApprovedBlogs.Where(x=>x.CurrentStatus == BlogStatus.Approved).ToList();

            return View(list);
        }

        public IActionResult SpecificBlog(int id)
        {
            SpecificBlogViewModel obj = new SpecificBlogViewModel();
            //LINQ diye join dite hobe  
            obj.ApprovedBlog = _context.ApprovedBlogs.Where(x=>x.Id == id).FirstOrDefault();
            obj.ApprovedBlog.Reactions = _context.PostReactions.Where(x=>x.PostId == id).ToList();
            obj.ApprovedBlog.PostComments = _context.PostComments.Where(x => x.PostId == id).ToList();

            return View(obj);
        }

        [Authorize]
        public IActionResult CreateNewBlog()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNewBlog([Bind("Content")]PendingBlog Obj)
        {
            string name = User.Identity.Name!;
            MyUser obj = await _userManager.FindByNameAsync(name);
            Obj.CreatedBy = obj.Id;
            Obj.CreatedDate = DateTime.Now;

            ApprovedBlog Obj2 = new ApprovedBlog();
            Obj2.Content = Obj.Content;
            Obj2.CreatedBy = Obj.CreatedBy;
            Obj2.CreatedDate = Obj.CreatedDate;

            _context.ApprovedBlogs.Add(Obj2);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
