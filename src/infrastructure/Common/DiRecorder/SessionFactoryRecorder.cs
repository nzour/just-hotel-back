using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate;
using Kernel.Abstraction;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernateConfiguration = NHibernate.Cfg.Configuration;

namespace Infrastructure.Common.DiRecorder
{
    public class SessionFactoryRecorder : AbstractServiceRecorder
    {
        private const string MappingNamespace = "Infrastructure.NHibernate.Mapping";
        private const string MappingPostfix = "Map";

        private TypeFinder DomainTypeFinder { get; }
        private TypeFinder InfrastructureFinder { get; }

        public SessionFactoryRecorder(TypeFinder domainTypeFinder, TypeFinder infrastructureFinder)
        {
            DomainTypeFinder = domainTypeFinder;
            InfrastructureFinder = infrastructureFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            var configuration = new NHibernateConfiguration().SetNamingStrategy(new PostgresNamingStrategy());

            var fluentConfiguration = Fluently.Configure(configuration)
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    .DefaultSchema(DbAccessor.Schema)
                    .ConnectionString(DbAccessor.ConnectionString)
                );

            RegisterMappings(fluentConfiguration);

            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            services.AddSingleton(typeof(ISessionFactory), sessionFactory);
            services.AddScoped(_ => sessionFactory.OpenSession());
            services.AddScoped<Transactional>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddPostgres()
                    .WithGlobalConnectionString(DbAccessor.ConnectionString)
                    .ScanIn(GetType().Assembly).For.Migrations())
                .AddLogging(logBuilder => logBuilder.AddFluentMigratorConsole());
        }

        /// <summary>
        ///     Добавит сборки всех наследников класса или интерфейса в конфигурацию маппингов Nhibernate (рекурсивно)
        /// </summary>
        /// <param name="configuration">Конфигурация Fluent NHibernate</param>
        private void RegisterMappings(FluentConfiguration configuration)
        {
            var mappings = InfrastructureFinder.FindTypes(t =>
                (bool) t.Namespace?.StartsWith(MappingNamespace) && t.Name.EndsWith(MappingPostfix)
            );

            foreach (var mapping in mappings)
            {
                configuration.Mappings(cfg => cfg.FluentMappings.AddFromAssembly(mapping.Assembly));
            }
        }
    }
}