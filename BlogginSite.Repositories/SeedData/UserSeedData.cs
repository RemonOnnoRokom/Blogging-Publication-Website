using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogginSite.Repositories.Db;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogginSite.Repositories.SeedData
{
    public static  class UserSeedData
    {
        
        public static async Task Initialize( UserManager<MyUser> _userManager)
        {
            //if (_userManager.Users.Any())
            //{
            //    return;
            //}

            MyUser[] users =  {
                new MyUser
                {
                    Name = "Reo",
                    UserName = "reo@gmail.com",
                    Email = "reo@gmail.com",
                },
                new MyUser
                {
                    Name = "Rojina",
                    UserName = "rojina@gmail.com",
                    Email = "rojina@gmail.com",
                },
                new MyUser
                {
                    Name = "Rakib",
                    UserName = "rakib@gmail.com",
                    Email = "rakib@gmail.com",
                },
                new MyUser
                {
                    Name = "Rishad",
                    UserName = "rishad@gmail.com",
                    Email = "rishad@gmail.com",
                }
            };

            foreach (var item in users)          
            {
                var result = _userManager.Users.Where(x => x.Name == item.Name).FirstOrDefault();
                if(result == null)     
                     await _userManager.CreateAsync(item, "123456");
            }

        }
        
    }
}
