using System;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using eShopOnContainers.CatalogService.API.Application.MessageHandlers;
using eShopOnContainers.CatalogService.API.Infrastructure.ESB;
using eShopOnContainers.CatalogService.API.Infrastructure.Persistence;
using eShopOnContainers.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MediatR;
using RabbitMQ.Client;

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
            services
                .AddDbContext<CatalogContext>(options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                        npgsqlOptionsAction =>
                        {
                            npgsqlOptionsAction.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            npgsqlOptionsAction.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                        });
                });

            return services;
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            IBusControl azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
            {
                string subscriptionName = "eShopOnContainers.CatalogService.API";

                busFactoryConfig.Message<ProductPriceChangedEvent>(configTopology =>
                {
                    configTopology.SetEntityName(nameof(ProductPriceChangedEvent)); 
                });

                busFactoryConfig.Host(configuration["EventBus:ConnectionString"]);

                busFactoryConfig.SubscriptionEndpoint<ProductPriceChangedEvent>(subscriptionName, c =>
                {
                    c.Consumer<ProductPriceChangedEventHandler>();
                });

                busFactoryConfig.UseRawJsonSerializer();
            });

            azureServiceBus.StartAsync().GetAwaiter();

            services.AddMassTransit(config =>
            {
                config.AddBus(provider => azureServiceBus);
            });


            services.AddSingleton<IPublishEndpoint>(azureServiceBus);
            services.AddSingleton<ISendEndpointProvider>(azureServiceBus);
            services.AddSingleton<IBus>(azureServiceBus);

            services.AddScoped<IEventBusService, EventBusService>();

            return services;
        }

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductPriceChangedEventHandler>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("13.76.4.84", "/",
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                    cfg.ExchangeType = ExchangeType.Direct;

                    cfg.ReceiveEndpoint("ProductPriceChangedEvent", e =>
                    {
                        e.ConfigureConsumer<ProductPriceChangedEventHandler>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddScoped<IEventBusService, EventBusService>();

            return services;
        }
    }
}
