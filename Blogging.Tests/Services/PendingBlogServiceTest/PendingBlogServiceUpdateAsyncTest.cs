using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using NSubstitute;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceUpdateAsyncTest:PendingBlogServiceBeseTest
    {
        public PendingBlogServiceUpdateAsyncTest()
        {
            
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

    }
}
