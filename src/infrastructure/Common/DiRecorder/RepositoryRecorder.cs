using System;
using System.Linq;
using System.Reflection;
using Domain;
using Domain.Rent;
using Domain.Room;
using Domain.User;
using FluentNHibernate.Conventions;
using Infrastructure.NHibernate.Repository;
using Kernel.Abstraction;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;
using Root.Configuration.ServiceRecorder.Exception;

namespace Infrastructure.Common.DiRecorder
{
    public class RepositoryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "Domain";
        private const string EndsWithPattern = "Repository";

        public TypeFinder DomainFinder { get; }
        public TypeFinder InfrastructureFinder { get; }

        public RepositoryRecorder(TypeFinder domainFinder, TypeFinder infrastructureFinder)
        {
            DomainFinder = domainFinder;
            InfrastructureFinder = infrastructureFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            ProcessEntityRepositoryExplicitly(services);

            var interfaces = DomainFinder.FindTypes(IsRepositoryInterface);

            foreach (var @interface in interfaces)
            {
                var implementation = FindImplementation(@interface);

                // Регистрируем реализацию репозитория
                services.AddScoped(@interface, implementation);
            }
        }

        /// <summary>
        ///     Поиск интерфейсов I*Repository с указанным Namespace'ом
        /// </summary>
        private bool IsRepositoryInterface(Type type)
        {
            if (null == type.Namespace) return false;

            return type.Namespace.Contains(NamespacePattern) &&
                   type.IsInterface && type.Name.EndsWith(EndsWithPattern);
        }

        /// <summary>
        ///     Поиск первой попавшейся реализации.
        ///     Если реализации нет, выбросит исключение.
        /// </summary>
        private TypeInfo FindImplementation(TypeInfo @interface)
        {
            var implementations = InfrastructureFinder.FindTypes(type =>
                type.IsClass && type.GetInterfaces().Contains(@interface));

            // todo: Сообщение выводится некорректно. Добавить нормальное логирование.
            return implementations.IsNotEmpty()
                ? implementations.First()
                : throw RepositoryRecorderException.NotFoundImplementation(@interface);
        }

        private void ProcessEntityRepositoryExplicitly(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository<UserEntity>, EntityRepository<UserEntity>>();
            services.AddScoped<IEntityRepository<RoomEntity>, EntityRepository<RoomEntity>>();
            services.AddScoped<IEntityRepository<RentEntity>, EntityRepository<RentEntity>>();
        }
    }
}