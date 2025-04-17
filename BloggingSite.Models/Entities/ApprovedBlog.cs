using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.ViewModel;

namespace BloggingSite.Models.Entities
{
    public class ApprovedBlog : PendingBlog
    {       
        public int ApprovedBy { get; set; }
        public DateTime PublishedDate { get; set; }
        public BlogStatus CurrentStatus { get; set; } = BlogStatus.Create;

        [NotMapped]
        public List<BlogPostComment> PostComments { get; set; }

        [NotMapped]
        public List<BlogPostReaction> Reactions { get; set; }

    }
}
