using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.Common.Attribute;
using FluentNHibernate.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.InjectedAttributeRecorder
{
    public class InjectedAttributeRecorder : AbstractServiceRecorder
    {
        protected override void Execute(IServiceCollection services)
        {
            TryInjectServices(FindClasses(), services.BuildServiceProvider());
        }

        private IEnumerable<TypeInfo> FindClasses()
        {
            return GetAssembly()
                .DefinedTypes
                .Where(type => !type.IsInterface && GetPropertiesWithInjectedAttribute(type).IsNotEmpty());
        }

        /// <summary>
        /// Для каждого класса, возьмет все поля, помеченные аттирбутами Injected
        /// И попробует внедрить в них сервисы.
        /// </summary>
        private void TryInjectServices(IEnumerable<TypeInfo> classes, IServiceProvider provider)
        {
            foreach (var @class in classes)
            {
                var properties = GetPropertiesWithInjectedAttribute(@class);

                foreach (var property in properties)
                {
                    InjectProperty(@class, property, provider);
                }
            }
        }

        /// <summary>
        ///  Попробует внедрить сервис в поле
        /// </summary>
        /// <param name="class"></param>
        /// <param name="property"></param>
        /// <param name="provider"></param>
        private void InjectProperty(object @class, PropertyInfo property, IServiceProvider provider)
        {
            try
            {
                var propertyType = property.PropertyType;
                //var service = provider.GetService(propertyType) ?? Activator.CreateInstance(propertyType);
                // todo: Not working. Fix in future.
                //  property.SetValue(@class, service);
            }
            catch
            {
                // todo: Log here
                // I know, this is useless, but Rider warnings this place.
                throw;
            }
        }

        /// <summary>
        /// Найдем все поля класса, которые помечены аттрибутом Injected
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private IEnumerable<PropertyInfo> GetPropertiesWithInjectedAttribute(TypeInfo type)
        {
            return type.GetRuntimeProperties()
                .Where(prop => prop.GetCustomAttributes().Contains(new InjectedAttribute()));
        }

        protected override IEnumerable<AbstractServiceRecorder> GetDependencies()
        {
            return new AbstractServiceRecorder[]
            {
                new CommandQueryRecorder.CommandQueryRecorder(),
                new RepositoryRecorder.RepositoryRecorder()
            };
        }
    }
}