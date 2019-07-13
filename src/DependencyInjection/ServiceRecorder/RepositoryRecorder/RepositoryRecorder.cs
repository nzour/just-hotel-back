using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.Common.Annotation;
using app.Infrastructure.NHibernate;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Linq.Functions;

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
                var instance = Handle(Activator.CreateInstance(implementation));
                // Регистрируем реализацию репозитория
                services.AddSingleton(@interface, instance);
            }
        }

        /// <summary>
        /// Поиск интерфейсов I*Repository
        /// </summary>
        private IEnumerable<TypeInfo> FindInterfaces()
        {
            return GetAssembly().DefinedTypes.Where(type =>
                type.IsInterface &&
                type.Namespace.Contains(InterfaceNamespacePattern) &&
                type.Name.EndsWith(RepositoryPattern)
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
                : throw RepositoryRecorderException.NotFound(@interface);
        }

        /// <summary>
        /// Внедрение сесси в репозитории в виде свойства. 
        /// </summary>
        private object Handle(object instance)
        {
            var properties = instance
                .GetType()
                .GetRuntimeProperties()
                .Where(property => property.GetCustomAttributes().Contains(new InjectedAttribute()));


            foreach (var property in properties)
            {
                if (property.Name.Contains("session") || property.Name.Contains("Session"))
                {
                    // todo: сначала нужно настроить соединение с БД.
                    property.SetValue(instance, NHibernateHelper.OpenSession());
                }
            }

            return instance;
        }
    }
}