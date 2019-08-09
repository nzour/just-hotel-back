using System;
using app.Aspect;
using app.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app
{
    public class Startup : AbstractAssemblyAware
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }    

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Kernel.Boot(services);

            services.AddTransient<TestMiddleware>();
            
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMiddleware<TestMiddleware>();
            
            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseHttpsRedirection()
                .UseMvc();
        }
    }
}