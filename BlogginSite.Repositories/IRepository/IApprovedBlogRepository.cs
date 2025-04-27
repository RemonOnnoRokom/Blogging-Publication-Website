using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BlogginSite.Repositories.IRepository
{
    public interface IApprovedBlogRepository
    {
        Task<List<ApprovedBlog>> GetAllAsync();
        Task<ApprovedBlog> GetByIdAsync(int id);
        Task AddAsync(ApprovedBlog entity);
        Task UpdateAsync(ApprovedBlog obj);
        Task DeleteAsync(int id);
    }
}
