using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Extensions;
using Kernel.Abstraction;
using Kernel.Internal;
using Kernel.Service;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel
{
    public class Kernel
    {
        public Kernel(Assembly applicationScope, IServiceCollection? services = null)
        {
            ApplicationScope = applicationScope;
            Services = services ?? new ServiceCollection();
            TypeFinder = new TypeFinder(ApplicationScope);

            Services.AddSingleton(TypeFinder);
        }

        public Assembly ApplicationScope { get; }
        public IServiceCollection Services { get; }
        public TypeFinder TypeFinder { get; }
        public IList<IModule> Modules { get; set; } = new List<IModule>();

        public TypeFinder[] DeclaredFinders =>
            Modules.Select(module => new TypeFinder(module.GetType().Assembly)).Append(TypeFinder).ToArray();

        public void Boot()
        {
            ProcessInternalRecorders();
            ProcessMvc();

            foreach (var module in Modules)
            {
                module.Boot(Services);
            }

            Services.AddHttpContextAccessor();
        }

        public Kernel LoadModule(IModule module)
        {
            if (!Modules.Contains(module))
            {
                Modules.Add(module);
            }

            return this;
        }

        public Kernel LoadModules(params IModule[] modules)
        {
            foreach (var module in modules)
            {
                LoadModule(module);
            }

            return this;
        }

        private void ProcessMvc()
        {
            var mvcBuilder = Services.AddRouting().AddMvc();

            mvcBuilder.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));

            foreach (var finder in DeclaredFinders)
            {
                finder.FindTypes(IsGlobalFilter)
                    .Foreach(filter => mvcBuilder.AddMvcOptions(o => o.Filters.Add(filter)));
            }
        }

        private void ProcessInternalRecorders()
        {
            new ResolvingAttributeServiceRecorder(DeclaredFinders).Process(Services);
        }

        private bool IsGlobalFilter(TypeInfo type)
        {
            return type.ImplementedInterfaces.Contains(typeof(IGlobalFilter));
        }
    }
}