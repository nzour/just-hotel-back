using System.Text;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using FluentMigrator.Runner;
using Infrastructure.NHibernate;
using Infrastructure.NHibernate.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("Dev");

            services
                .AddNHibernate(connectionString)
                .AddFluentMigrator(connectionString)
                .AddEntityRepositories()
                .AddShaPasswordEncoder()
                .AddJwtTokenService(configuration);

            return services;
        }

        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var sessionFactory = NHibernateSessionFactory.CreateSessionFactory(connectionString);

            services.AddSingleton(sessionFactory);
            services.AddScoped(_ => sessionFactory.OpenSession());
            services.AddScoped<TransactionFacade>();

            return services;
        }

        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(InfrastructureExtension).Assembly).For.Migrations())
                .AddLogging(logBuilder => logBuilder.AddFluentMigratorConsole());

            return services;
        }

        public static IServiceCollection AddEntityRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<UserEntity>, Repository<UserEntity>>();
            services.AddTransient<IRepository<RoomEntity>, Repository<RoomEntity>>();
            services.AddTransient<IRepository<ServiceEntity>, Repository<ServiceEntity>>();
            services.AddTransient<IRepository<ReservationEntity>, Repository<ReservationEntity>>();

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddShaPasswordEncoder(this IServiceCollection services)
        {
            services.AddTransient<IPasswordEncoder, ShaPasswordEncoder>();

            return services;
        }

        public static IServiceCollection AddJwtTokenService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var config = new JwtTokenConfig();
            configuration.Bind("JwtToken", config);

            services.AddTransient<IJwtTokenService>(_ => new JwtTokenService(config));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(config.Secret);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            return services;
        }
    }
}
