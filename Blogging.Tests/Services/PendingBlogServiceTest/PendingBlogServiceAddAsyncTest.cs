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
    public class PendingBlogServiceAddAsyncTest:PendingBlogServiceBeseTest
    {
        public PendingBlogServiceAddAsyncTest()
        {
            
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
