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
            try
            {
                var list = await _service.GetAllAsync();

                return View(list);
            }
            catch(Exception)
            {
                return View("404NotFound");
            }
            
        }

        public async Task<IActionResult> Delete(int id)
         {
            try
            {
                await _service.DeleteAsync(id);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View("404NotFound");
            }
            
        }
       
        public IActionResult SeeMore(int id)
        {
            try
            {
                var obj = _service.GetByIdAsync(id);

                return View(nameof(Approved), obj);
            }
            catch (Exception)
            {
                return View("404NotFound");
            }
        }
        
        public async Task<IActionResult> Approved([Bind("PostId,AdminStatus")]AdminApprovedVM obj)
        {
            try
            {
                MyUser adminUser = await _userManager.FindByNameAsync(User.Identity.Name);
                obj.AdminId = adminUser.Id;
                await _service.ApprovedAsync(obj);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("404NotFound");
            }

        }

        public async Task<IActionResult> Top5Blogs([FromServices] ApplicationDbContext context)
        {
            try
            {
                int skipBlogs = 0;
                int numberOfTopBlog = 5;

                var ids = await context.Database.SqlQuery<TopBlogVM>($"select  [a].[Id] , [a].[Content]  \r\n from((select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs inner join  PostComments on ApprovedBlogs.Id =PostComments.PostId) \r\n union all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc;select  [a].[Id] , [a].[Content]  \r\nfrom(\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\t\t\tinner join  PostComments\r\n\t\t\t\t\ton ApprovedBlogs.Id =PostComments.PostId) \r\nunion all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc OFFSET {skipBlogs} ROWs \r\n\tFETCH Next  {numberOfTopBlog - 1} rows only").ToListAsync();

                return View(ids);
            }
            catch (Exception)
            {
                return View("404NotFound");
            }

        }

        public async Task<IActionResult> UserList([FromServices]ApplicationDbContext context)
        {
            try
            {
                List<MyUser> list;
                list = context.Users.Where(x => context.UserRoles.Any(y => y.UserId == x.Id) == false).ToList();

                return View(list);
            }
            catch (Exception)
            {
                return View("404NotFound");
            }

        }

        public async Task<IActionResult> Permission(int id)
        {
            try
            {
                MyUser obj = await _userManager.FindByIdAsync(id.ToString());
                if (obj.LockoutEnd is null)
                {
                    obj.LockoutEnd ??= DateTimeOffset.Now.AddMinutes(120 * 1440);
                }
                else
                {
                    obj.LockoutEnd = null;
                }

                IdentityResult result = await _userManager.UpdateAsync(obj);

                return RedirectToAction(nameof(UserList));
            }
            catch (Exception)
            {
                return View("404NotFound");
            }
        }
    }
}


