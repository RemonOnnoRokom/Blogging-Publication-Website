using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSite.Models.Entities
{
    public class BlogPostComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public ApprovedBlog? ApprovedBlog { get; set; }
        public string Comment { get; set; }
    }
}
