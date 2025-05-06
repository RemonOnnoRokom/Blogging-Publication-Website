using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using NSubstitute;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceSingleInstanceLoadingFunctionTest : PendingBlogServiceBeseTest
    {
        public PendingBlogServiceSingleInstanceLoadingFunctionTest()
        {
            
        }

        [Fact]
        public async Task GetByIdAsync_RepoReturnApprovedBlog_ReturnApprovedBlog()
        {
            //Arrange 
            const int id = 1;
            var expectedApprovedBlog = GetByIdDummyData();

            _approvedBlogRepository.GetByIdAsync(id).Returns(expectedApprovedBlog);

            //Act
            var result = await _sut.GetByIdAsync(id);

            //Assert
            Assert.Equal(expectedApprovedBlog.Id, result.Id);
            Assert.Equal(expectedApprovedBlog.Content, result.Content);
            Assert.Equal(expectedApprovedBlog.CreatedDate, result.CreatedDate);
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
