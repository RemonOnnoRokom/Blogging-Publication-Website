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
        private readonly UserManager<MyUser> _userManager;
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

        public async Task<IActionResult> Top5Blogs([FromServices] ApplicationDbContext context)
        {
            int skipBlogs = 0 ;
            int numberOfTopBlog = 5;

            var ids = await context.Database.SqlQuery<TopBlogVM>($"select  [a].[Id] , [a].[Content]  \r\n from((select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs inner join  PostComments on ApprovedBlogs.Id =PostComments.PostId) \r\n union all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc;select  [a].[Id] , [a].[Content]  \r\nfrom(\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\t\t\tinner join  PostComments\r\n\t\t\t\t\ton ApprovedBlogs.Id =PostComments.PostId) \r\nunion all\r\n\t\t(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs \r\n\t\t\tinner join  PostReactions\r\n\t\t\ton ApprovedBlogs.Id =PostReactions.PostId\r\n\t\t)\r\n) \r\n\t\tas a group by a.Id , a.Content  order by count(a.Id) desc OFFSET {skipBlogs} ROWs \r\n\tFETCH Next  {numberOfTopBlog - 1} rows only").ToListAsync();

            return View(ids);
        }
    }
}

//select approvedBlogs.Id, ApprovedBlogs.Content From ApprovedBlogs 
// INNER JOIN PostComments 
//	on ApprovedBlogs.Id = PostComments.PostId
//INNER JOIN PostReactions
//	on ApprovedBlogs.Id = PostReactions.PostId 
//	group by approvedBlogs.Id, ApprovedBlogs.Content order by Id desc
//	OFFSET 1 ROWs 
//	FETCH Next  5 rows only;


//select  [a].[Id] , [a].[Content]  
//from((select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs inner join  PostComments on ApprovedBlogs.Id =PostComments.PostId) 
//union all
//		(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs 
//			inner join  PostReactions
//			on ApprovedBlogs.Id =PostReactions.PostId
//		)
//) 
//		as a group by a.Id , a.Content  order by count(a.Id) desc;select  [a].[Id] , [a].[Content]  
//from(
//		(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs 
//					inner join  PostComments
//					on ApprovedBlogs.Id =PostComments.PostId) 
//union all
//		(select ApprovedBlogs.Id , ApprovedBlogs.Content   From ApprovedBlogs 
//			inner join  PostReactions
//			on ApprovedBlogs.Id =PostReactions.PostId
//		)
//) 
//		as a group by a.Id , a.Content  order by count(a.Id) desc;