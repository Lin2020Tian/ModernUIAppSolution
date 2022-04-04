using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModernUIApp.Services;
using Microsoft.FeatureManagement;
using Microsoft.ApplicationInsights.Extensibility;

namespace ModernUIApp
{
    public class Startup
    {
        // IoC Container
        // Also known as Dependency Injection (DI) Container, it is a programming framework that provides
        // you with an automatic Dependency Injection of your components.


        // Only the following framework service types can be injected in the Startup() constructor
        // and in the Configure() method of a standard ASP.NET application: IWebHostEnvironment, IHostEnvironment, and IConfiguration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the
        // Dependency Injection container, or known as the IoC (Inversion of Control)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ICustomerDataService, CustomerDataService>();
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();
            services.AddAzureAppConfiguration();
            services.AddFeatureManagement();
            services.AddSingleton<ITelemetryInitializer, ModernTelemetryInitializer>();
            Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();
             
            aiOptions.EnableAdaptiveSampling = true;
            aiOptions.EnableDebugLogger = true;
            aiOptions.EnableHeartbeat = true;
            aiOptions.EnablePerformanceCounterCollectionModule = true;
            aiOptions.EnableRequestTrackingTelemetryModule = true;
            aiOptions.EnableQuickPulseMetricStream = true;
             
            services.AddApplicationInsightsTelemetry(aiOptions);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseAzureAppConfiguration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
