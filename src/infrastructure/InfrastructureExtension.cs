using Domain;
using Domain.Rent;
using Domain.Room;
using Domain.Service;
using Domain.User;
using FluentMigrator.Runner;
using Infrastructure.NHibernate;
using Infrastructure.NHibernate.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<IEntityRepository<UserEntity>>();
            services.AddTransient<IEntityRepository<RentEntity>>();
            services.AddTransient<IEntityRepository<RoomEntity>>();
            services.AddTransient<IEntityRepository<ServiceEntity>>();

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

            return services;
        }
    }
}
