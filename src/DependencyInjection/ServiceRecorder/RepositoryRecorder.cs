using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.DependencyInjection.ServiceRecorder.Exception;
using FluentNHibernate.Conventions;
using kernel.Abstraction;
using kernel.Extensions;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder
{
    public class RepositoryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "app.Domain.Entity";
        private const string EndsWithPattern = "Repository";

        protected override void Execute(IServiceCollection services)
        {
            var finder = services.GetService<TypeFinder>();
            var interfaces = FindInterfaces(finder);

            foreach (var @interface in interfaces)
            {
                var implementation = FindImplementation(@interface, finder);

                // Регистрируем реализацию репозитория
                services.AddScoped(@interface, implementation);
            }
        }

        /// <summary>
        /// Поиск интерфейсов I*Repository с указанным Namespace'ом
        /// </summary>
        private IEnumerable<TypeInfo> FindInterfaces(TypeFinder finder)
        {
            return finder.FindTypes(type => type.IsInterface &&
                    (bool) type.Namespace?.Contains(NamespacePattern) &&
                    type.Name.EndsWith(EndsWithPattern));
        }

        /// <summary>
        /// Поиск первой попавшейся реализации.
        /// Если реализации нет, выбросит исключение.
        /// </summary>
        private TypeInfo FindImplementation(TypeInfo @interface, TypeFinder finder)
        {
            var implementations = finder
                .FindTypes(type =>type.IsClass &&
                    type.GetInterfaces().Contains(@interface));

            // todo: Сообщение выводится некорректно. Добавить нормальное логирование.
            return implementations.IsNotEmpty()
                ? implementations.First()
                : throw RepositoryRecorderException.NotFoundImplementation(@interface);
        }
    }
}