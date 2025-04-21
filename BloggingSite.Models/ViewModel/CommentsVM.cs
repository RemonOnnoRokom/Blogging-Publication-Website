using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingSite.Models.ViewModel
{
    public class CommentsVM
    {
        public int Id { get; set; }
        public long Commentator { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
    }
}
