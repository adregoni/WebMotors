using AutoMapper;
using System.Collections.Generic;
using WebMotors.Domain.AutoMapper.Profiles;

namespace WebMotors.Tests.Support
{
    public static class AutoMapperHelper
    {
        public static IMapper GetMapper()
        {
            return GetMapperConfiguration().CreateMapper();
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(mc =>
                                    mc.AddProfiles(new List<Profile>
                                    {
                                            new AnuncioProfile()
                                    }));

            return config;
        }
    }
}
