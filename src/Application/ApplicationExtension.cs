using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddCommandsAndQueries();

            return services;
        }

        public static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
        {
            foreach (var action in CommandQueryFinder.FindCommandAndQueries())
            {
                services.AddTransient(action);
            }

            return services;
        }
    }

    public static class CommandQueryFinder
    {
        private const string NamespacePattern = "Application.CQS";
        private const string QueryPattern = "Query";
        private const string CommandPattern = "Command";

        public static IEnumerable<Type> FindCommandAndQueries()
        {
            return typeof(CommandQueryFinder).Assembly.DefinedTypes.Where(IsCommandOrQuery);
        }

        private static bool IsCommandOrQuery(Type type)
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