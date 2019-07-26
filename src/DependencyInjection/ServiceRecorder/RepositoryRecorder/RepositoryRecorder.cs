using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.RepositoryRecorder
{
    public class RepositoryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "app.Domain.Entity";
        private const string EndsWithPattern = "Repository";

        protected override void Execute(IServiceCollection services)
        {
            var interfaces = FindInterfaces();

            foreach (var @interface in interfaces)
            {
                var implementation = FindImplementation(@interface);
                var instance = Activator.CreateInstance(implementation);
                // Регистрируем реализацию репозитория
                services.AddSingleton(@interface, instance);
            }
        }

        /// <summary>
        /// Поиск интерфейсов I*Repository с указанным Namespace'ом
        /// </summary>
        private IEnumerable<TypeInfo> FindInterfaces()
        {
            return GetAssembly()
                .DefinedTypes
                .Where(type =>
                    type.IsInterface &&
                    type.Namespace.Contains(NamespacePattern) &&
                    type.Name.EndsWith(EndsWithPattern)
                );
        }

        /// <summary>
        /// Поиск первой попавшейся реализации.
        /// Если реализации нет, выбросит исключение.
        /// </summary>
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
                : throw RepositoryRecorderException.NotFoundImplementation(@interface);
        }
    }
}