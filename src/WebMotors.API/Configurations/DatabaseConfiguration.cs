using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Data.Contexts;

namespace WebMotors.API.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("WebMotorsSQL");

            services.AddDbContext<WebMotorsContext>(option =>
            option.UseSqlServer(connectionString, mig => mig.MigrationsAssembly("WebMotors.Data")));
        }
    }
}
