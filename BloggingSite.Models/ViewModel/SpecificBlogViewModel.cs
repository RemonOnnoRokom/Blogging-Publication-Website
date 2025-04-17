using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Models.ViewModel
{
    public class SpecificBlogViewModel 
    {
        public ApprovedBlog? ApprovedBlog { get; set; }
        public BlogPostComment? Comment { get; set; }
        public BlogPostReaction? Reaction { get; set; }
    }
}
