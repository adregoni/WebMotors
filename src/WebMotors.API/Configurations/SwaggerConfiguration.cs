using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebMotors.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(
             options =>
             {
                 options.GroupNameFormat = "'v'VVV";
                 options.SubstituteApiVersionInUrl = true;
             });

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            var startupAssembly = Assembly.GetEntryAssembly();

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var assemblyDetails = startupAssembly.GetCustomAttribute<AssemblyProductAttribute>();

                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = $"{assemblyDetails.Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = "WebMotors - Anuncios"
                    });
                }
            });
        }

        public static void UseSwaggerPage(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    sw.SwaggerEndpoint($"./swagger/{description.GroupName}/swagger.json", $"WebMotors.Api - {description.GroupName.ToUpperInvariant()}");
                }

                sw.RoutePrefix = string.Empty;
            });
        }
    }
}
