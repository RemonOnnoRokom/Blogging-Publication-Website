using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceDeleteAsyncTest : PendingBlogServiceBeseTest
    {
        public PendingBlogServiceDeleteAsyncTest()
        {
            
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
    }
}
