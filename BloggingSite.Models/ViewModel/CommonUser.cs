using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Models.ViewModel
{
    public class CommonUser
    {
        public long? MyUserId { get; set; }

        [ForeignKey("MyUserId")]
        public MyUser? MyUsers { get; set; }
    }
}
