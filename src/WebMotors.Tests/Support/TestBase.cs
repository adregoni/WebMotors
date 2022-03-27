using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WebMotors.Data.Contexts;

namespace WebMotors.Tests.Support
{
    public class TestBase
    {
        public WebMotorsContext ContextInMemory { get; set; }

        public Mock<IConfiguration> Configuration { get; set; }

        [TestInitialize]
        public virtual void Setup()
        {
            var options = new DbContextOptionsBuilder<WebMotorsContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            ContextInMemory = new WebMotorsContext(options);

            Configuration = new Mock<IConfiguration>();
        }
    }
}
