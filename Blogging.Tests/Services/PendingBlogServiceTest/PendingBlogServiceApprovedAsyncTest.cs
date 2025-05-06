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
    public class PendingBlogServiceApprovedAsyncTest : PendingBlogServiceBeseTest
    {
        public PendingBlogServiceApprovedAsyncTest()
        {
            
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
