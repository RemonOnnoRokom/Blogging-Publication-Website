using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Models.ViewModel
{
    public class ApprovedBlogVM
    {
        public IEnumerable<ApprovedBlog> ApprovedBlogs { get; set; }
        public int ItemNumber { get; set; }
    }
}
