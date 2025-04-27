using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
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
        public async Task<List<ApprovedBlog>> GetAllAsync()
        {
            var list = await _context.ApprovedBlogs.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<ApprovedBlog> GetByIdAsync(int id)
        {
            try
            {
                var obj = await _context.ApprovedBlogs.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task AddAsync(ApprovedBlog entity)
        {
            await _context.ApprovedBlogs.AddAsync(entity);
            await _context.SaveChangesAsync();           
        }

        public async Task DeleteAsync(int id)
        {
            var obj = await _context.ApprovedBlogs.FirstOrDefaultAsync(x=>x.Id == id);
            _context.ApprovedBlogs.Remove(obj);
            await _context.SaveChangesAsync();
        }                

        public async Task UpdateAsync(ApprovedBlog obj)
        {
            try
            {
                //written by sabbir vai
                _context.Entry(obj).State = EntityState.Modified;             
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           
        }
    }
}

