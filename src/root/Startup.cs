using System.Text.Json.Serialization;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
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
                    options.Filters.Add(new AuthorizeFilter());
                });

            services
                .AddApplication()
                .AddInfrastructure(Configuration)
                .AddHttpContextAccessor()
                .AddTransient<ExceptionHandlingMiddleware>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles()
                .UseCors(builder =>
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                )
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseAuthorization()
                .UseAuthentication()
                .UseHsts()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}