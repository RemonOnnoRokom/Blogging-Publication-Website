using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    [Authorize]
    public class BlogPostReactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BlogPostReactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Likes([Bind("PostId,Expression")]BlogPostReaction Obj)
        {
            //BlogPostReaction Obj = new BlogPostReaction()
            //{
            //    PostId = PostId,
            //    Expression = type
            //};
            _context.PostReactions.Add(Obj);
            _context.SaveChanges();
            
            return  RedirectToAction($"SpecificBlog","Home",new { id = Obj.PostId });      
        }

        public IActionResult Comments([Bind("PostId,Comment")]BlogPostComment Obj)
        {
            _context.PostComments.Add(Obj);
            _context.SaveChanges();

            return RedirectToAction($"SpecificBlog", "Home", new { id = Obj.PostId });
        }
        
    }
}
