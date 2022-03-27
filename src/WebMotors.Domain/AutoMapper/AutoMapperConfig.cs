using System;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Domain.AutoMapper.Profiles;

namespace WebMotors.Domain.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddAutoMapper(new Type[]
            {
                typeof(AnuncioProfile)
            });
        }

    }

}
