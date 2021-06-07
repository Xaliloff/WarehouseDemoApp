using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using Warehouse.Infrastructure.Context;

namespace Warehouse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            try
            {
                ConfigureLogger();

                Log.Information($"{appName} app starting up.");
                var host = CreateHostBuilder(args).Build();

                PrepareDb(host);
                host.Run();
                Log.Information($"{appName} app stopped cleanly.");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{appName} app crashed while starting up.");
                Log.CloseAndFlush();
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void ConfigureLogger()
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static void PrepareDb(IHost host)
        {
            var appName = Assembly.GetExecutingAssembly().GetName().Name;
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<WarehouseContext>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    if (context.Database.IsSqlServer() && context.Database.GetPendingMigrations().Any())
                    {
                        Log.Information($"{appName} app started db migrations.");
                        context.Database.Migrate();
                        Log.Information($"{appName} app db migrations completed successfully.");
                    }

                    //await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating or seeding the database in Warehouse service.");

                    throw;
                }
            }
        }
    }
}