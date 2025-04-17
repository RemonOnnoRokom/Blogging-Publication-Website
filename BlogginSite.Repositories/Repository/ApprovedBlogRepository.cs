using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BlogginSite.Repositories.Db;
using BlogginSite.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogginSite.Repositories.Repository
{
    public class ApprovedBlogRepository : IApprovedBlogRepository
    {
        private readonly ApplicationDbContext _context;
        public ApprovedBlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ApprovedBlog entity)
        {
             _context.ApprovedBlogs.Add(entity);
            await _context.SaveChangesAsync();
           
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            _context.ApprovedBlogs.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApprovedBlog>> GetAllAsync()
        {
            var list = await _context.ApprovedBlogs.ToListAsync();
            return list;
        }
            

        public async Task<ApprovedBlog> GetByIdAsync(int id)
        {
           var Obj = await _context.ApprovedBlogs.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Obj;
        }

        public void Update(ApprovedBlog entity)
        {
            _context.ApprovedBlogs.Update(entity);
            _context.SaveChanges();
        }
    }
}
