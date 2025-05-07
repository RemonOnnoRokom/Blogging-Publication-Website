using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BloggingSite.Services.IService;
using BlogginSite.Repositories.IRepository;

namespace BloggingSite.Services.Service
{
    public class PendingBlogService : IPendingBlogService
    {
        private readonly IApprovedBlogRepository _repository;
        public PendingBlogService(IApprovedBlogRepository repository)
        {
            _repository = repository;
        }

        #region List Loading Function
        public async Task<IEnumerable<PendingBlog>> GetAllAsync()
        {
            List<PendingBlog> result = null;
            try
            {
                List<ApprovedBlog> list = await _repository.GetAllAsync();

                result = list.Where(x => x.CurrentStatus == BlogStatus.Create)
                                                .Select(x =>
                                                            new PendingBlog()
                                                            {
                                                                Id = x.Id,
                                                                CreatedBy = x.CreatedBy,
                                                                Content = x.Content,
                                                                CreatedDate = x.CreatedDate
                                                            })
                                                .ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception occured in PendingBlogService");
                throw;
            }
            
            return result;
        }
        #endregion

        #region Single Instance Loading Function
        public async Task<PendingBlog> GetByIdAsync(int id)
        {
            try
            {
                ApprovedBlog Obj = await _repository.GetByIdAsync(id);

                PendingBlog result = null;
                if (Obj.CurrentStatus == BlogStatus.Create)
                {
                    result = new PendingBlog()
                    {
                        Id = Obj.Id,
                        CreatedBy = Obj.CreatedBy,
                        Content = Obj.Content,
                        CreatedDate = Obj.CreatedDate
                    };
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        #endregion

        #region Operational Function
        public async Task AddAsync(PendingBlog entity)
        {
            try
            {
                ApprovedBlog Obj = new ApprovedBlog();

                Obj.CreatedBy = entity.CreatedBy;
                Obj.Content = entity.Content;
                Obj.CreatedDate = entity.CreatedDate;

                await _repository.AddAsync(Obj);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        
        public async Task UpdateAsync(ApprovedBlog obj)
        {
            try
            {
                await _repository.UpdateAsync(obj);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
       
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public async Task ApprovedAsync(AdminApprovedVM obj)
        {
            try
            {
                var dbObj = await _repository.GetByIdAsync(obj.PostId);
                dbObj.CurrentStatus = obj.AdminStatus;
                dbObj.ApprovedBy = obj.AdminId;
                await _repository.UpdateAsync(dbObj);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        #endregion       
    }
}