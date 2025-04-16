﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;

namespace BloggingSite.Services.IService
{
    public interface IApprovedBlogService
    {
        Task<IEnumerable<PendingBlog>> GetAllAsync();
        Task<PendingBlog> GetByIdAsync(int id);
        Task AddAsync(ApprovedBlog entity);
        void Update(ApprovedBlog entity);
        Task DeleteAsync(int id);
    }
}
