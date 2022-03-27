using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.API.Validators;

namespace WebMotors.API.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services
                .AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<AnuncioValidator>();
                });
        }
    }
}
