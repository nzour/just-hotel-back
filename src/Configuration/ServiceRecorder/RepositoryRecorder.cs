using System;
using System.Linq;
using System.Reflection;
using app.Configuration.ServiceRecorder.Exception;
using FluentNHibernate.Conventions;
using kernel.Abstraction;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace app.Configuration.ServiceRecorder
{
    public class RepositoryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "app.Domain";
        private const string EndsWithPattern = "Repository";

        public TypeFinder TypeFinder { get; }

        public RepositoryRecorder(TypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            var interfaces = TypeFinder.FindTypes(IsRepositoryInterface);

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
    }
}