using System;
using System.Linq;
using System.Reflection;
using App.Domain;
using App.Infrastructure.Common;
using App.Infrastructure.NHibernate;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Kernel.Abstraction;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace App.Configuration.ServiceRecorder
{
    public class SessionFactoryRecorder : AbstractServiceRecorder
    {
        private TypeFinder TypeFinder { get; }

        public SessionFactoryRecorder(TypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82
                    // Понятия не имею что это и для чего. Но без этого не работает ¯\_(ツ)_/¯
                    .Raw("hbm2ddl.keywords", "none")
                    .ConnectionString(DbAccessor.ConnectionString));

            RegisterEntitiesRecursively(fluentConfiguration, typeof(AbstractEntity));

            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            services.AddSingleton(typeof(ISessionFactory), sessionFactory);
            services.AddSingleton(new Transactional(sessionFactory));

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
                if (MustIgnoreEntity(entity))
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

        private bool MustIgnoreEntity(TypeInfo entity)
        {
            return entity.IsInterface || entity.GetCustomAttributes(true).Contains(new IgnoreMappingAttribute());
        }
    }
}