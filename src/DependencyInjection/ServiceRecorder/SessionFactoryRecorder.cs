using System;
using app.Domain.Entity;
using app.Infrastructure.Common;
using app.Infrastructure.NHibernate;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using kernel.Abstraction;
using kernel.Extensions;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace app.DependencyInjection.ServiceRecorder
{
    public class SessionFactoryRecorder : AbstractServiceRecorder
    {
        private TypeFinder TypeFinder { get; set; }

        protected override void Execute(IServiceCollection services)
        {
            TypeFinder = services.GetService<TypeFinder>();

            var fluentConfiguration = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    // Понятия не имею что это и для чего. Но без этого не работает ¯\_(ツ)_/¯
                    .Raw("hbm2ddl.keywords", "none")
                    .ConnectionString(DbAccessor.ConnectionString));

            RegisterEntitiesRecursively(fluentConfiguration, typeof(AbstractEntity));

            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            services.AddSingleton(typeof(ISessionFactory), sessionFactory);
            services.AddSingleton(typeof(Transactional), new Transactional(sessionFactory));

            services.AddFluentMigratorCore()
                .ConfigureRunner(runner => runner
                    .AddPostgres()
                    .WithGlobalConnectionString(DbAccessor.ConnectionString)
                    .ScanIn(TypeFinder.ApplicationScope).For.Migrations())
                .AddLogging(logBuilder => logBuilder.AddFluentMigratorConsole());
        }

        /// <summary>
        /// Добавит сборки всех наследников класса или интерфейса в конфигурацию маппингов Nhibernate (рекурсивно)
        /// </summary>
        /// <param name="configuration">Конфигурация Fluent NHibernate</param>
        /// <param name="class">Тип класса</param>
        private void RegisterEntitiesRecursively(FluentConfiguration configuration, Type @class)
        {
            var entities = TypeFinder.FindTypes(t => t.IsSubclassOf(@class));

            foreach (var entity in entities)
            {
                if (entity.IsAbstract || entity.IsInterface)
                {
                    RegisterEntitiesRecursively(configuration, entity);
                }
                else
                {
                    configuration.Mappings(cfg => cfg.FluentMappings.AddFromAssembly(entity.Assembly));
                    RegisterEntitiesRecursively(configuration, entity);
                }
            }
        }
    }
}