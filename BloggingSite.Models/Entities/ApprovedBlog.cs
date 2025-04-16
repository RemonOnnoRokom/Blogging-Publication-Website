using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSite.Models.Entities
{
    public class ApprovedBlog
    {
        public int Id { get; set; }        
        public string Content { get; set; }       
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<BlogPostComment>? PostComments { get; set; }
        public List<BlogPostReaction>? Reactions { get; set; }

    }
}
