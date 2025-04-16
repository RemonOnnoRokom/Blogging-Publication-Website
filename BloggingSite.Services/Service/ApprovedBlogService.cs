using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BlogginSite.Repositories.IRepository;

namespace BloggingSite.Services.Service
{
    public class ApprovedBlogService : IApprovedBlogRepository
    {
        private readonly IApprovedBlogRepository _repository;
        public ApprovedBlogService(IApprovedBlogRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(ApprovedBlog entity)
        {
            await _repository.AddAsync(entity);            
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ApprovedBlog>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ApprovedBlog> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Update(ApprovedBlog entity)
        {
             _repository.Update(entity);
        }
    }
}
