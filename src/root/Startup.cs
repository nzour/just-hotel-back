using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Root.Configuration;
using _Kernel = Kernel.Kernel;

namespace Root
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
            var kernel = new _Kernel(GetType().Assembly, services);

            kernel
                .LoadModules(new ApplicationModule(), new InfrastructureModule())
                .Boot();

            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Zobor" }));

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles()
                .UseCors(builder => builder.WithOrigins("http://localhost:4200"))
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseAuthorization()
                .UseAuthentication()
                .UseHsts()
                .UseRouting()
                .UseSwagger()
                .UseSwaggerUI(setup => setup.SwaggerEndpoint("/doc", "Zobor doc"))
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}