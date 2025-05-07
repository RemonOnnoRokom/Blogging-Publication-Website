using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BloggingSite.Services.IService;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggingSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PendingBlogPostController : Controller
    {
        private readonly IPendingBlogService _service;
        private readonly UserManager<MyUser> _userManager;
        public PendingBlogPostController(IPendingBlogService service , UserManager<MyUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<PendingBlog> list = new List<PendingBlog>();
            try
            {
                list = await _service.GetAllAsync();
            }
            catch(Exception)
            {
                ViewData["Error"] = "Something Error occurs";
            }

            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
         {
            try
            {
                await _service.DeleteAsync(id);
            }
            catch(Exception)
            {
                TempData["Error"] = "Not working Properly";                
            }
            
            return RedirectToAction(nameof(Index));
        }
       
        public async Task<IActionResult> SeeMore(int id)
        {
            PendingBlog blog = new PendingBlog();
            try
            {
                blog = await _service.GetByIdAsync(id);                
            }
            catch (Exception)
            {
                ViewData["Error"] = "Not Found";
            }

            return View(nameof(Approved), blog);
        }
        
        public async Task<IActionResult> Approved([Bind("PostId,AdminStatus")]AdminApprovedVM obj)
        {
            try
            {
                MyUser? adminUser = await _userManager.FindByNameAsync(User?.Identity?.Name!);
                obj.AdminId = adminUser!.Id;
                await _service.ApprovedAsync(obj);               
            }
            catch (Exception)
            {
                TempData["Error"] = "Not Approved";                
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Top5Blogs([FromServices] ApplicationDbContext context)
        {
            List<TopBlogVM> topBlogEntities = new List<TopBlogVM>();
            try
            {
                int skipBlogs = 0;
                int numberOfTopBlog = 5;

                topBlogEntities = await context.Database.SqlQuery<TopBlogVM>($"select  [a].[Id] , [a].[Content]  \r\n from((select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs inner join  PostComments on ApprovedBlogs.Id =PostComments.PostId) \r\n union all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc;select  [a].[Id] , [a].[Content]  \r\nfrom(\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\t\t\tinner join  PostComments\r\n\t\t\t\t\ton ApprovedBlogs.Id =PostComments.PostId) \r\nunion all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc OFFSET {skipBlogs} ROWs \r\n\tFETCH Next  {numberOfTopBlog - 1} rows only").ToListAsync();     
            }
            catch (Exception)
            {
                ViewData["Error"] = "Not working Properly due to the Error";              
            }

            return View(topBlogEntities);
        }

        public IActionResult UserList([FromServices]ApplicationDbContext context)
        {
            List<MyUser> list = new List<MyUser>();
            try
            {               
                list = context.Users.Where(x => context.UserRoles.Any(y => y.UserId == x.Id) == false).ToList();
            }
            catch (Exception)
            {
                ViewData["Error"] = "Not working Properly due to the Error";
            }

            return View(list);
        }

        public async Task<IActionResult> Permission(int id)
        {
            try
            {
                MyUser? obj = await _userManager.FindByIdAsync(id.ToString());
                if (obj?.LockoutEnd is null)
                {
                    obj!.LockoutEnd ??= DateTimeOffset.Now.AddMinutes(120 * 1440);
                }
                else
                {
                    obj.LockoutEnd = null;
                }

                IdentityResult result = await _userManager.UpdateAsync(obj);  
            }
            catch (Exception)
            {
                TempData["Error"] = "Not working Properly";
            }

            return RedirectToAction(nameof(UserList));
        }
    }
}


