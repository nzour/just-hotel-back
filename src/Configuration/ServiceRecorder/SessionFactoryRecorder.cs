using System;
using System.Collections.Generic;
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
using NHibernate.Cfg;
using NHibernateConfiguration = NHibernate.Cfg.Configuration;

namespace App.Configuration.ServiceRecorder
{
    public class SessionFactoryRecorder : AbstractServiceRecorder
    {
        private IEnumerable<TypeInfo> IgnoredEntities => new[] { typeof(AbstractEntity).GetTypeInfo() };

        private TypeFinder TypeFinder { get; }

        public SessionFactoryRecorder(TypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            var configuration = new NHibernateConfiguration().SetNamingStrategy(new PostgresNamingStrategy());

            var fluentConfiguration = Fluently.Configure(configuration)
                .Database(
                    PostgreSQLConfiguration.PostgreSQL82
                        .DefaultSchema(DbAccessor.Schema)
                        .ConnectionString(DbAccessor.ConnectionString)
                );

            RegisterEntitiesRecursively(fluentConfiguration, typeof(AbstractEntity));

            ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();

            services.AddSingleton(typeof(ISessionFactory), sessionFactory);
            services.AddSingleton(new Transactional(sessionFactory));

            services.AddFluentMigratorCore()
                .ConfigureRunner(
                    runner => runner
                        .AddPostgres()
                        .WithGlobalConnectionString(DbAccessor.ConnectionString)
                        .ScanIn(TypeFinder.ApplicationScope).For.Migrations()
                )
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
                    continue;
                }

                configuration.Mappings(cfg => cfg.FluentMappings.AddFromAssembly(entity.Assembly));
                RegisterEntitiesRecursively(configuration, entity);
            }
        }

        private bool MustIgnoreEntity(TypeInfo entity)
        {
            return entity.IsInterface || IgnoredEntities.Contains(entity);
        }
    }
}