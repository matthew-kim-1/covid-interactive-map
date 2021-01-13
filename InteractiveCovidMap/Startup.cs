using AutoMapper;
using CovidTracking.BusinessLayer.Interfaces;
using CovidTracking.BusinessLayer.Services;
using CovidTracking.CustomException;
using CovidTracking.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CovidTracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient();
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();

            services.AddTransient<ICurrentStateService, CurrentStateService>();
            services.AddTransient<IStateCodeNameService, StateCodeNameService>();
            services.AddTransient<ICovidTrackingApiService, CovidTrackingApiService>();
            services.AddTransient<ICountyService, CountyService>();
            services.AddTransient<ICacheService, CacheService>();

            services.AddDbContext<CovidTrackingContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("CovidTrackingConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlerMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}