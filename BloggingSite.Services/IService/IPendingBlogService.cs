﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;

namespace BloggingSite.Services.IService
{
    public interface IPendingBlogService
    {
        Task<IEnumerable<PendingBlog>> GetAllAsync();
        Task<PendingBlog> GetByIdAsync(int id);
        Task AddAsync(PendingBlog entity);
        Task UpdateAsync(ApprovedBlog entity);
        Task DeleteAsync(int id);
        Task ApprovedAsync(AdminApprovedVM obj);
    }
}
