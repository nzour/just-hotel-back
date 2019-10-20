using System;
using Kernel.Abstraction;
using Kernel.Extension;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace App.Configuration.ServiceRecorder
{
    public class CommandQueryRecorder : AbstractServiceRecorder
    {
        private const string NamespacePattern = "App.Application.CQS";
        private const string QueryPattern = "Query";
        private const string CommandPattern = "Command";

        protected override void Execute(IServiceCollection services)
        {
            var actions = services
                .GetService<TypeFinder>()!
                .FindTypes(IsCommandOrQuery);

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