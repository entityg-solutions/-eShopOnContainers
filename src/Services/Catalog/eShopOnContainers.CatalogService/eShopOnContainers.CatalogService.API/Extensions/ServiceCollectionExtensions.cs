using System;
using System.Reflection;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace eShopOnContainers.CatalogService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eShopOnContainers.CatalogService.API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CatalogContext>(options =>
                {
                    options.UseNpgsql(configuration["ConnectionString"],
                        npgsqlOptionsAction =>
                        {
                            npgsqlOptionsAction.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            npgsqlOptionsAction.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                        });
                });

            return services;
        }
    }
}
