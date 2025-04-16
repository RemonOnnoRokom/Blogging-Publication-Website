using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSite.Models.Entities
{
    //Meta Data About ApprovedBlogPost
    public class BlogPostReaction
    {
        public int Id { get; set; }
        public int BlogId{ get; set; }
        [ForeignKey(nameof(BlogId))]
        public ApprovedBlog? Post { get; set; }
        public Expression Expression { get; set; }

    }
}
