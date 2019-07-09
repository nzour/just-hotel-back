using System;
using System.Linq;
using app.CQS;
using app.CQS.User;
using app.CQS.User.Command;
using app.CQS.User.Query;
using app.Modules.ORM.Repositories.Implementations;
using app.Modules.ORM.Repositories.Interfaces;
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
            InjectServices(services);

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

        private void InjectServices(IServiceCollection services)
        {
            RegisterForService(services, typeof(IExecutable));
            
            services.AddSingleton<IUserRepository>(provider =>
                new UserRepository(NHibernateHelper.OpenSession()));
        }

        private void RegisterForService(IServiceCollection services, Type serviceType)
        {
            var assembly = typeof(Startup).Assembly;
            var implementations = assembly.DefinedTypes.Where(type => type.GetInterfaces().Contains(serviceType));
            foreach (var implementation in implementations)
            {
                services.AddTransient(implementation);
            }
        }
    }
}