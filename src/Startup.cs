using System.IO;
using app.Configuration.Middleware;
using kernel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app
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

            var kernel = new Kernel(GetType().Assembly, services);

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