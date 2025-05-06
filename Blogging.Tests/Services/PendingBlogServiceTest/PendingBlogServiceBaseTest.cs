using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloggingSite.Services.IService;
using BloggingSite.Services.Service;
using BlogginSite.Repositories.IRepository;
using NSubstitute;

namespace Blogging.Tests.Services.PendingBlogServiceTest
{
    public class PendingBlogServiceBaseTest
    {

        protected readonly IPendingBlogService _sut;
        protected readonly IApprovedBlogRepository _approvedBlogRepository;

        public PendingBlogServiceBaseTest()
        {
            _approvedBlogRepository = Substitute.For<IApprovedBlogRepository>();
            _sut = new PendingBlogService(_approvedBlogRepository);
        }
    }
}
