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
        public async Task AddAsync(PendingBlog entity)
        {
            ApprovedBlog Obj = new ApprovedBlog();

            Obj.CreatedBy = entity.CreatedBy;
            Obj.Content = entity.Content;
            Obj.CreatedDate = entity.CreatedDate;
            
            await _repository.AddAsync(Obj);                            
        }

        public async Task Approved(AdminApprovedVM obj)
        {            
            var dbObj =  _repository.GetByIdAsync(obj.PostId);
            dbObj.CurrentStatus = obj.AdminStatus;
            dbObj.ApprovedBy = obj.AdminId;

            _repository.Update(dbObj);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PendingBlog>> GetAllAsync()
        {
            List<ApprovedBlog> list =  await _repository.GetAllAsync();

            List<PendingBlog> result = list.Where(x=>x.CurrentStatus == BlogStatus.Create)
                                            .Select(x =>
                                                        new PendingBlog()
                                                        {
                                                            Id = x.Id,
                                                            CreatedBy = x.CreatedBy,
                                                            Content = x.Content,
                                                            CreatedDate = x.CreatedDate
                                                        })
                                            .ToList(); 

            return result;
        }

        public async Task<PendingBlog> GetByIdAsync(int id)
        {
            ApprovedBlog Obj = _repository.GetByIdAsync(id);

            PendingBlog result = null;
            if(Obj.CurrentStatus == BlogStatus.Create)
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

        public void Update(ApprovedBlog entity)
        {
             _repository.Update(entity);
        }       
    }
}
