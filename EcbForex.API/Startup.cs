using System.Globalization;
using EcbForex.API.Clients;
using EcbForex.API.Domain.Clients;
using EcbForex.API.Domain.Models;
using EcbForex.API.Domain.Services;
using EcbForex.API.Middleware;
using EcbForex.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;

namespace EcbForex.API
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
            services.AddControllers();
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            services.AddScoped<IRestClient, RestClient>();
            services.AddScoped<IEcbClient, EcbClient>();
            services.AddScoped<IRateService, RateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Add Error Handling Middleware
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var cultureInfo = new CultureInfo("sv-SE");

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
