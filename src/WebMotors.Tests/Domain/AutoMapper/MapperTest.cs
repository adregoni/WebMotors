using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMotors.Tests.Support;

namespace WebMotors.Tests.Domain.AutoMapper
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void ValidateMapper()
        {
            var mapper = AutoMapperHelper.GetMapperConfiguration();
            mapper.AssertConfigurationIsValid();
        }
    }
}
