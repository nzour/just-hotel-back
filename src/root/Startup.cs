using System.Text.Json.Serialization;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Root.Configuration;
using Root.Configuration.Converter;

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
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                })
                .AddMvcOptions(options =>
                {
                    options.Filters.Add<ModelStateValidationFilter>();
                    options.Filters.Add<TransactionalCommandFilter>();
                    options.Filters.Add<UserAwareActionFilter>();
                });

            services
                .AddApplication()
                .AddInfrastructure(Configuration)
                .AddTransient<ExceptionHandlingMiddleware>();
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