using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using app.Common.Annotation;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.CommandQueryRecorder
{
    public class CommandQueryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "app.Application.CQS";
        private const string QueryPattern = "Query";
        private const string CommandPattern = "Command";
        
        protected override void Execute(IServiceCollection services)
        {
            var types = FindServices();

            foreach (var type in types)
            {
                ResolveAttributes(services, type);
            }
        }

        /// <summary>
        /// Команды и запросы можно помечать одним из аттрибутов Transient, Scoped или Singleton.
        /// В зависимости от того, каким аттрибутом помечена команда,
        /// Зарегистрирует её в соответственном стиле.
        /// Если ни один из атрибутов не найден, то зарегистрирует как Transient.
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

            if (attributes.Contains(new SingletonAttribute()))
            {
                services.AddSingleton(type);
                return;
            }

            services.AddTransient(type);
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

                    return type.Name.EndsWith(QueryPattern) || type.Name.EndsWith(CommandPattern);
                });
        }
    }
}