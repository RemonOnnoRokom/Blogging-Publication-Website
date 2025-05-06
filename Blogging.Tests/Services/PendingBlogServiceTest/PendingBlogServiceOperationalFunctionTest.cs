using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using NSubstitute;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceOperationalFunctionTest : PendingBlogServiceBaseTest
    {
        #region AddAsync
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
        #endregion

        #region UpdateAsync
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
        #endregion

        #region DeleteAsync
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
        #endregion

        #region ApprovedAsync
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
    }
}
