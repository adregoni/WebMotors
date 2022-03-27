using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Anticorruption.OnlineChallenge.Adapters;
using WebMotors.Data.Contexts;
using WebMotors.Data.UoW;
using WebMotors.Domain.Contracts;
using WebMotors.Domain.Contracts.OnlineChallenge;
using WebMotors.Domain.Contracts.Repository;
using WebMotors.Domain.Contracts.UnityOfWork;
using WebMotors.Domain.Services;

namespace WebMotors.Common.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddScoped(typeof(IRepository<>), typeof(Data.Repositories.Base.Repository<>));

            services.AddScoped<IAnuncioService, AnuncioService>();

            services.AddScoped<IOnlineChallengeAdapter, OnlineChallengerAdapter>();

            services.AddScoped<IUnitOfWork, UnitOfWork<WebMotorsContext>>();

            return services;
        }
    }
}
