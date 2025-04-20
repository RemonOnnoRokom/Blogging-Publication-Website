using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BloggingSite.Models.Entities
{
    public class MyUser : IdentityUser<long>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}

