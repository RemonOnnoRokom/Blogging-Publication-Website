﻿using System;
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
using NSubstitute.ExceptionExtensions;
using Xunit.Sdk;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceListLoadingFunctionTest : PendingBlogServiceBaseTest
    {       

        [Fact]
        public async Task GetAllAsync_RepoReturnApprovedBlogList_ReturnValidList()
        {
            //Arrange
            var actualData = GetAllDummyData();

            _approvedBlogRepository.GetAllAsync().Returns(actualData);

            //Act
            var expect =  await _sut.GetAllAsync();

            //Assert
            Assert.Equal(expect.Count() , actualData.Count());
        }

        [Fact]
        public async Task GetAllAsync_RepoThrowException_ReThrowException()
        {
            //Arrange
             _approvedBlogRepository.GetAllAsync().ThrowsAsync(new Exception());
            
            //Act & Assert
            await Assert.ThrowsAsync<Exception>( ()=> _sut.GetAllAsync());
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
    }
}
