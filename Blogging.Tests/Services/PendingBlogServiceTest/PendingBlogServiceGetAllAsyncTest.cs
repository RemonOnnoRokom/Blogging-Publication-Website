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
    public class PendingBlogServiceGetAllAsyncTest : PendingBlogServiceBeseTest
    {
        public PendingBlogServiceGetAllAsyncTest()
        {
            
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
            const int id = 2;

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
