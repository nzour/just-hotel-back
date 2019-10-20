using System.IO;
using _Kernel = Kernel.Kernel;
using App.Configuration.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var envFile = $"{Directory.GetCurrentDirectory()}/environment.json";

            var kernel = new _Kernel(GetType().Assembly, services);

            kernel
                .LoadEnvironmentVariables(envFile)
                .Boot();

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles()
                .UseCors(builder => builder.WithOrigins("http://localhost:4200"))
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseAuthentication()
                .UseHsts()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}