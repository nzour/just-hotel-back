using System;
using Microsoft.Extensions.DependencyInjection;

namespace kernel.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static T GetService<T>(this IServiceCollection services) where T : class
        {
            return services.BuildServiceProvider().GetService(typeof(T)) as T;
        }

        public static TCast GetService<TCast>(this IServiceCollection services, Type type) where TCast : class
        {
            return services.BuildServiceProvider().GetService(type) as TCast;
        }
    }
}