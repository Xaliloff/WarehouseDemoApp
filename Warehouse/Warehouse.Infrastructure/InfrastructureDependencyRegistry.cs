using GreenPipes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Warehouse.App.Common.Interfaces;
using Warehouse.Infrastructure.Context;

namespace Warehouse.Infrastructure
{
    public static class InfrastructureDependencyRegistry
    {
        public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WarehouseContext>(options =>
                options.UseSqlServer(config.GetConnectionString("SqlConnection")));

            services.AddScoped<IWarehouseContext>(x => x.GetRequiredService<WarehouseContext>());


            ConfigureBus(services, config);

            return services;
        }

        private static void ConfigureBus(IServiceCollection services, IConfiguration config)
        {
            //const string HandledBy = "ByWarehouseService";

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseMessageRetry(r =>
                    {
                        r.Incremental(5, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1000));
                    });
                    cfg.Host(new Uri(config.GetConnectionString("MQConnection")));
                    //cfg.ReceiveEndpoint($"{nameof(ItemsQuantityChangedHandler)}{HandledBy}", ep =>
                    //{
                    //    ep.ConfigureConsumer<ItemsQuantityChangedHandler>(context);
                    //});
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}