using System;
using System.Linq;
using System.Reflection;
using app.DependencyInjection.ServiceRecorder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            RecordServices(services, typeof(Startup).Assembly);
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseHttpsRedirection()
                .UseMvc();
        }

        private void RecordServices(IServiceCollection services, Assembly assembly)
        {
            var recorders = assembly.DefinedTypes
                .Where(type => type.GetInterfaces().Contains(typeof(IServiceRecorder)));

            foreach (var recorder in recorders)
            {
                var instance = Activator.CreateInstance(recorder);
                recorder.GetDeclaredMethod("Process").Invoke(instance, new object[] {services});
            }
        }
    }
}