using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSite.Models.ViewModel
{
    public class PendingBlog
    {
        public int Id { get; set; }

        [Required]
        [MinLength(30)]
        public string Content { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
