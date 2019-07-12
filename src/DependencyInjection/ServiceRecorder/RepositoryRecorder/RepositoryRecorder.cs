using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.Infrastructure.NHibernate;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.RepositoryRecorder
{
    public class RepositoryRecorder : AbstractAssemblyAware, IServiceRecorder
    {
        private const string InterfaceNamespacePattern = "app.Domain.Entity";
        private const string RepositoryPattern = "Repository";

        /// <summary>
        /// Регистрирует репозитории.
        /// </summary>
        public void Process(IServiceCollection services)
        {
            var interfaces = FindInterfaces();

            foreach (var @interface in interfaces)
            {
                var implementation = FindImplementation(@interface);
                // Регистрируем реализацию репозитория
                services.AddSingleton(
                    @interface,
                    provider => Activator.CreateInstance(
                        implementation, NHibernateHelper.OpenSession()
                    )
                );
            }
        }

        private IEnumerable<TypeInfo> FindInterfaces()
        {
            return GetAssembly().DefinedTypes.Where(type =>
                type.IsInterface &&
                type.Namespace.Contains(InterfaceNamespacePattern) &&
                type.Name.EndsWith(RepositoryPattern)
            );
        }

        private TypeInfo FindImplementation(TypeInfo @interface)
        {
            var implementations = GetAssembly()
                .DefinedTypes
                .Where(type =>
                    type.IsClass &&
                    type.GetInterfaces().Contains(@interface));

            // todo: Сообщение выводится некорректно. Добавить нормальное логирование.
            return implementations.IsNotEmpty()
                ? implementations.First()
                : throw RepositoryRecorderException.NotFound(@interface);
        }
    }
}