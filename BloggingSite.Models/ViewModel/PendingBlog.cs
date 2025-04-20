using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Models.ViewModel
{
    public class PendingBlog: CommonUser
    {
        public int Id { get; set; }
  
        [Required]
        [MinLength(30)]
        public string Content { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
