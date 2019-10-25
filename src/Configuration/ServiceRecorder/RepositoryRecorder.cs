using System;
using System.Linq;
using System.Reflection;
using App.Configuration.ServiceRecorder.Exception;
using App.Domain;
using App.Domain.DebtEntity;
using App.Domain.RoomEntity;
using App.Domain.UserEntity;
using App.Infrastructure.NHibernate.Repository;
using FluentNHibernate.Conventions;
using Kernel.Abstraction;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace App.Configuration.ServiceRecorder
{
    public class RepositoryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "App.Domain";
        private const string EndsWithPattern = "Repository";

        public TypeFinder TypeFinder { get; }

        public RepositoryRecorder(TypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            var interfaces = TypeFinder.FindTypes(IsRepositoryInterface);

            ProcessEntityRepositoryExplicitly(services);

            foreach (var @interface in interfaces)
            {
                var implementation = FindImplementation(@interface);

                // Регистрируем реализацию репозитория
                services.AddScoped(@interface, implementation);
            }
        }

        /// <summary>
        /// Поиск интерфейсов I*Repository с указанным Namespace'ом
        /// </summary>
        private bool IsRepositoryInterface(Type type)
        {
            if (null == type.Namespace)
            {
                return false;
            }

            return type.Namespace.Contains(NamespacePattern) &&
                   type.IsInterface && type.Name.EndsWith(EndsWithPattern);
        }

        /// <summary>
        /// Поиск первой попавшейся реализации.
        /// Если реализации нет, выбросит исключение.
        /// </summary>
        private TypeInfo FindImplementation(TypeInfo @interface)
        {
            var implementations = TypeFinder.FindTypes(type =>
                type.IsClass && type.GetInterfaces().Contains(@interface)
            );

            // todo: Сообщение выводится некорректно. Добавить нормальное логирование.
            return implementations.IsNotEmpty()
                ? implementations.First()
                : throw RepositoryRecorderException.NotFoundImplementation(@interface);
        }

        private void ProcessEntityRepositoryExplicitly(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<User>, EntityRepository<User>>();
            services.AddScoped<IEntityRepository<Room>, EntityRepository<Room>>();
            services.AddScoped<IEntityRepository<Debt>, EntityRepository<Debt>>();
        }
    }
}