﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Models.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit.Sdk;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceSingleInstanceLoadingFunctionTest : PendingBlogServiceBaseTest
    {
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

        [Fact]
        public async Task GetByIdAsync_RepoReturnException_ReThrowException()
        {
            //Arrange
            const int id = 1;

            _approvedBlogRepository.GetByIdAsync(id).ThrowsAsync(new Exception());

            //Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _sut.GetByIdAsync(id));
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
