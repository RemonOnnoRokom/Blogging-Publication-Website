using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BlogginSite.Repositories.IRepository
{
    public interface IApprovedBlogRepository
    {
        Task<IEnumerable<ApprovedBlog>> GetAllAsync();
        Task<ApprovedBlog> GetByIdAsync(int id);
        Task AddAsync(ApprovedBlog entity);
        void Update(ApprovedBlog entity);
        Task DeleteAsync(int id);
    }
}
