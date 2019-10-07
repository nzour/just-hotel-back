using System.IO;
using app.Application.Middleware;
using kernel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            kernel.Boot();
            kernel.LoadEnvironmentVariables(envFile);

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<HandledExceptionMiddleware>();

            app.UseDefaultFiles()
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseMvc();
        }
    }
}