using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.ViewModel;

namespace BloggingSite.Models.Entities
{
    //Meta Data About ApprovedBlogPost
    public class BlogPostReaction: CommonUser
    {
        public int Id { get; set; }
        public int PostId{ get; set; }
        [ForeignKey(nameof(PostId))]
        public ApprovedBlog? Post { get; set; }
        public Expression Expression { get; set; }

    }
}
