using System;
using System.Reflection;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace kernel
{
    public class Kernel
    {
        public Assembly Assembly { get; }
        private readonly IServiceCollection _services;

        public IServiceProvider Services => _services.BuildServiceProvider();

        public Kernel(Assembly assembly, IServiceCollection services = null)
        {
            Assembly = assembly;
            _services = services ?? new ServiceCollection();
            _services.AddSingleton(new TypeFinder(Assembly));

        }

        public void Boot()
        {
            // todo implement
        }
    }
}