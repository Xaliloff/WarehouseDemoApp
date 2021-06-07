using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Warehouse.API.Services;
using Warehouse.App;
using Warehouse.App.Services;
using Warehouse.Infrastructure;

namespace Warehouse.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            ConfigureAuthentication(services);

            services.AddHttpContextAccessor();

            services.RegisterAppDependencies();
            services.RegisterInfrastructureDependencies(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order.API", Version = "v1" });
            });
        }

        protected virtual void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                            .AddJwtBearer("Bearer", config =>
                            {
                                config.Authority = Configuration.GetValue<string>("IdentityServerURL");
                                config.RequireHttpsMetadata = false;
                                config.TokenValidationParameters = new()
                                {
                                    ValidateAudience = false
                                };
                            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Warehouse.API v1"));
            }

            app.UseDeveloperExceptionPage();
            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}