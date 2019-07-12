using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.Common.Annotation;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.CommandQueryRecorder
{
    public class CommandQueryRecorder : AbstractAssemblyAware, IServiceRecorder
    {
        private const string NamespacePattern = "app.Application.CQS";
        private const string QueryPattern = "Query";
        private const string CommandPattern = "Command";
        
        /// <summary>
        /// Регистрирует Command и Query
        /// </summary>
        public void Process(IServiceCollection services)
        {
            var types = FindServices();

            foreach (var type in types)
            {
                if (MustResolveAttribute(type))
                {
                    ResolveAttributes(services, type);
                    continue;
                }

                // Если Доп аттрибутов нет, то Transient
                services.AddTransient(type);
            }
        }

        /// <summary>
        /// В зависимости от того, каким аттрибутом помечена команда,
        /// Зарегистрирует её в соответственном стиле.
        /// </summary>
        private void ResolveAttributes(IServiceCollection services, Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type);

            if (attributes.Contains(new TransientAttribute()))
            {
                services.AddTransient(type);
                return;
            }

            if (attributes.Contains(new ScopedAttribute()))
            {
                services.AddScoped(type);
                return;
            }

            services.AddSingleton(type);
        }

        /// <summary>
        /// Команды и запросы можно помечать одним из аттрибутов Transient, Scoped или Singleton.
        /// Если бы найден хоть один из аттрибутов, то вернет true.
        /// </summary>
        private bool MustResolveAttribute(Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type);
            return attributes.Contains(new TransientAttribute()) ||
                   attributes.Contains(new ScopedAttribute()) ||
                   attributes.Contains(new SingletonAttribute());
        }

        private IEnumerable<TypeInfo> FindServices()
        {
            return GetAssembly()
                .DefinedTypes
                .Where(type =>
                {
                    if (!type.Namespace.Contains(NamespacePattern))
                    {
                        return false;
                    }

                    return type.Name.EndsWith(QueryPattern) ||
                           type.Name.EndsWith(CommandPattern);
                });
        }
    }
}