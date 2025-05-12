using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using BloggingSite.Models.ViewModel;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

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

        [Fact]
        public async Task AddAsync_RepoThrowException_ReThrowException()
        {
            //Arrange
            var entityPending = GetPendingBlog();

            _approvedBlogRepository.AddAsync(Arg.Any<ApprovedBlog>()).ThrowsAsync(new Exception());

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _sut.AddAsync(entityPending));

        }
        #endregion

        #region UpdateAsync
        [Fact]
        public async Task UpdateAsync_ValidEntity_UpdateSuccessful()
        {
            //Arrange
            var getByIdSpecificData = GetByIdDummyData();

            //Act
            await _sut.UpdateAsync(getByIdSpecificData);

            //Assert
            await _approvedBlogRepository.Received(1).UpdateAsync(getByIdSpecificData);
        }

        [Fact]
        public async Task UpdateAsync_RepoThrowException_ReThrowException()
        {
            //Arrange
            var getByIdSpecificData = GetByIdDummyData();

            _approvedBlogRepository.UpdateAsync(Arg.Any<ApprovedBlog>()).ThrowsAsync(new Exception());

            //Assert & Act
            await Assert.ThrowsAsync<Exception>(() => _sut.UpdateAsync(getByIdSpecificData));
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

        [Fact]
        public async Task DeleteAsync_RepoThrowException_ReThrowException()
        {
            //Arrange
            const int id = 2;

            _approvedBlogRepository.DeleteAsync(Arg.Any<int>()).ThrowsAsync(new Exception());

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(()=>_sut.DeleteAsync(id));
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

        [Fact]
        public async Task ApprovedAsync_RepoThrowException_ReThrowException()
        {
            //Arrange
            var entityData = DummyAdminApprovedVM();

            _approvedBlogRepository.GetByIdAsync(Arg.Any<int>()).ThrowsAsync(new Exception());

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _sut.ApprovedAsync(entityData));
        }
        #endregion

        #region Helper Region 

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

        #endregion
    }
}
