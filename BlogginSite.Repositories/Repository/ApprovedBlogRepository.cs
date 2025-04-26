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
            var obj = GetByIdAsync(id);
            _context.ApprovedBlogs.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ApprovedBlog>> GetAllAsync()
        {
            var list = await _context.ApprovedBlogs.ToListAsync();
            return list;
        }
            
        public ApprovedBlog GetByIdAsync(int id)
        {
            try
            {
                var obj =_context.ApprovedBlogs.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
                return obj;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }           
        }

        public void Update(ApprovedBlog entity)
        {
            try
            {               
                 _context.ApprovedBlogs.Update(entity);
                  _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }
    }
}
