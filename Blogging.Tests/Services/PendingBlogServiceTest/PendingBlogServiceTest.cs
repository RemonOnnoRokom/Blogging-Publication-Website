using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using BloggingSite.Services.IService;
using BloggingSite.Services.Service;
using BlogginSite.Repositories.IRepository;
using Microsoft.Identity.Client;
using NSubstitute;
using Xunit.Sdk;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceTest
    {
        private readonly IPendingBlogService _sut;
        private readonly IApprovedBlogRepository _approvedBlogRepository;

        public PendingBlogServiceTest()
        {
            _approvedBlogRepository = Substitute.For<IApprovedBlogRepository>();
            _sut = new PendingBlogService(_approvedBlogRepository);
        }

        [Fact]
        public async Task GetAllAsync_RepoReturnApprovedBlogList_ValidList()
        {
            //Arrange
            var expectedData = GetAllDummyData();

            _approvedBlogRepository.GetAllAsync().Returns(expectedData);

            //Act
            var result =  await _sut.GetAllAsync();

            //Assert
            Assert.Equal(expectedData.Count() , result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_RepoReturnApprovedBlog_ReturnApprovedBlog()
        {
            //Arrange 
            const int id = 1;
            var expectedApprovedBlog = GetByIdDummyData();

            _approvedBlogRepository .GetByIdAsync(id).Returns(expectedApprovedBlog);

            //Act
            var result = await _sut.GetByIdAsync(id);

            //Assert
            Assert.Equal(expectedApprovedBlog.Id , result.Id);
            Assert.Equal(expectedApprovedBlog.Content, result.Content);
            Assert.Equal(expectedApprovedBlog.CreatedDate, result.CreatedDate);
        }

        [Fact]
        public async Task AddAsync_ValidEntity_AddSuccessful()
        {
            //Arrange
            var entityPending = GetPendingBlog();
                       
            //Act 
            var result = _sut.AddAsync(entityPending);

            //Assert
            await _approvedBlogRepository.Received(1).AddAsync(Arg.Any<ApprovedBlog>());
        }

        [Fact]
        public async Task UpdateAsync_ValidEntity_UpdateSuccessful()
        {
            //Arrange
            var GetByIdSpecificData = GetByIdDummyData();

            //Act
            await _sut.UpdateAsync(GetByIdSpecificData);

            //Assert
            await _approvedBlogRepository.Received(1).UpdateAsync(GetByIdSpecificData);
        }

        [Fact]
        public async Task DeleteAsync_ValidEntity_DeleteSuccessful()
        {
            //Arrange
            int id = 2;

            //Act
            await _sut.DeleteAsync(id);

            //Assert
            await _approvedBlogRepository.Received(1).DeleteAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task ApprovedAsync_BlogStatusApproved_BlogApprovedSuccessful()
        {
            //Arrange
            var entityData = DummyAdminApprovedVM();            
            var entityGetById = GetByIdDummyData();

            _approvedBlogRepository.GetByIdAsync(entityData.PostId).Returns(entityGetById);

            //Act
            await _sut.ApprovedAsync(entityData);

            //Assert
            await _approvedBlogRepository.Received(1).UpdateAsync(Arg.Any<ApprovedBlog>());
        }

        #region helper( GetAllDummyData)
        public List<ApprovedBlog> GetAllDummyData()
        {
            List<ApprovedBlog> dummy = new List<ApprovedBlog>()
            {
                new ApprovedBlog
                {
                     MyUserId = 1,
                     ApprovedBy =0,
                     PublishedDate = DateTime.Now,
                     CurrentStatus = BlogStatus.Create

                },
                new ApprovedBlog
                {
                    MyUserId = 2,
                    ApprovedBy =0,
                    PublishedDate = DateTime.Now,
                    CurrentStatus = BlogStatus.Create
                }
            };

            return dummy;
        }
        #endregion

        #region helper(Specific Id)
        public ApprovedBlog GetByIdDummyData()
        {
            var specificBlog = new ApprovedBlog()
            {
                Id = 1,
                CreatedBy = 10,
                Content = "Humpti dumpti drum drum",
                CreatedDate = DateTime.Now

            };
            return specificBlog;
        }
        #endregion

        #region helper(AddAsync)
        public PendingBlog GetPendingBlog()
        {
            PendingBlog entity = new PendingBlog()
            {
                CreatedBy = 2,
                Content = "Mamma",
                CreatedDate = DateTime.Now
            };

            return entity;
        }
        #endregion

        #region helper(AdminApprovedVm)
        public AdminApprovedVM DummyAdminApprovedVM()
        {
            AdminApprovedVM entity = new AdminApprovedVM()
            {
                PostId = 3,
                AdminId = 0,
                AdminStatus = BlogStatus.Approved
            };

            return entity;
        }
        
        #endregion
    }
}
