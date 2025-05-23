using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Tracking.Api.Extensions;
using Tracking.Api.Utils;
using Tracking.Application.Common.Settings;

namespace Tracking.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomAuthentication(Configuration);
            services.AddCustomAuthorization();
            services.AddCustomMVC();
            services.AddCustomServices();
            services.AddCustomHealthCheck();
            services.AddCustomOptions(Configuration);
            services.AddLayersDependencyInjections(Configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = Configuration.Get<AppSettings>().ApplicationDisplayName,
                    Version = "v1"
                });
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
            });


            var container = new ContainerBuilder();
            container.Populate(services);

            ApplicationContainer = container.Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings appSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("../swagger/v1/swagger.json", appSettings.ApplicationDisplayName);
                });
            }
            app.UseSerilogRequestLogging();
            app.UseResponseCompression();
            app.UserCustomExceptionHandler(env);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(Constants.CorsPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            //var applicationDisplayName = configurationManager.GetValue<string>(Constants.ApplicationDisplayName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGet(Constants.WelcomePath, async context =>
                {
                    await context.Response.WriteAsync(string.Format(Constants.WelcomeAPI, appSettings.ApplicationDisplayName));
                });
                endpoints.MapHealthChecks(Constants.HealthPath, new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResponseWriter = ConfigureExtensions.WriteResponseHealth
                });
                endpoints.MapHealthChecks(Constants.ExternalHealthPath, new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResponseWriter = ConfigureExtensions.WriteResponseHealth
                });
            });
        }
    }
}
