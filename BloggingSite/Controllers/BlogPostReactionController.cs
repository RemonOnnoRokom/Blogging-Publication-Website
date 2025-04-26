using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BlogginSite.Repositories.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggingSite.Controllers
{
    [Authorize(Roles="Admin,User")]
    public class BlogPostReactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public BlogPostReactionController(ApplicationDbContext context , UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Likes(string myUserName, int postId , Expression expression)
        {
            BlogPostReaction Obj = new BlogPostReaction();
            Obj.PostId = postId;
            Obj.Expression = expression;
            var user = await _userManager.FindByNameAsync(myUserName);

            Obj.MyUserId = user.Id;

            var persistOrNot = _context.PostReactions.Where(x => x.PostId == postId && x.MyUserId == Obj.MyUserId).FirstOrDefault();

            if (persistOrNot != null) 
            {
                if (persistOrNot.Expression == expression)
                    Delete(persistOrNot);
                else
                {
                    persistOrNot.Expression = expression;
                    Update(persistOrNot);
                }                
            }
            else
            {
                _context.PostReactions.Add(Obj);
                _context.SaveChanges();
            }
           
            return  RedirectToAction($"SpecificBlog","Home",new { id = Obj.PostId });      
        }

        private void Update(BlogPostReaction Obj)
        {
             _context.PostReactions.Update(Obj);
             _context.SaveChanges();            
        }

        private void Delete(BlogPostReaction Obj)
        {
            _context.PostReactions.Remove(Obj);
            _context.SaveChanges();
        }

        public async Task<IActionResult> Comments([Bind("PostId,Comment")]BlogPostComment Obj)
        {
            if(Obj.Comment is not null)
            {
                Obj.MyUserId = (await _userManager.FindByNameAsync(User.Identity.Name))!.Id;
                _context.PostComments.Add(Obj);
                _context.SaveChanges();
            }
           
            return RedirectToAction($"SpecificBlog", "Home", new { id = Obj.PostId });
        }
        
    }
}
