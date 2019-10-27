using System;
using Kernel;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class ApplicationModule : IModule
    {
        private const string NamespacePattern = "Application.CQS";
        private const string QueryPattern = "Query";
        private const string CommandPattern = "Command";

        public void Boot(IServiceCollection services)
        {
            RegisterCqsServices(services);
        }

        private void RegisterCqsServices(IServiceCollection services)
        {
            var actions = new TypeFinder(GetType().Assembly).FindTypes(IsCommandOrQuery);

            foreach (var action in actions)
            {
                services.AddTransient(action);
            }
        }

        private bool IsCommandOrQuery(Type type)
        {
            if (null == type.Namespace)
            {
                return false;
            }

            return type.Namespace.Contains(NamespacePattern) &&
                   (type.Name.EndsWith(QueryPattern) || type.Name.EndsWith(CommandPattern));
        }
    }
}