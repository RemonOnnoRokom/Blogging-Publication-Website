using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Models.ViewModel
{
    public class AdminApprovedVM
    {
        public int PostId { get; set; }
        public long AdminId { get; set; }
        public BlogStatus AdminStatus { get; set; }
    }
}
