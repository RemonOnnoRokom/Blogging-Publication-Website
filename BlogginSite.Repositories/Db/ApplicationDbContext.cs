using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogginSite.Repositories.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<ApprovedBlog> ApprovedBlogs { get; set; }        
        public DbSet<BlogPostReaction> PostReactions { get; set; }
        public DbSet<BlogPostComment> PostComments { get; set; }
    }
}
