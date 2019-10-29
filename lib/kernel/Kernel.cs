using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
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

        public Kernel LoadEnvironmentVariables(string envFilename)
        {
            if (!File.Exists(envFilename))
                throw new FileNotFoundException(
                    $"Unable to read environment file. File with name {envFilename} was not found.");

            File.ReadAllText(envFilename);

            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(envFilename));

            foreach (var (key, value) in json) Environment.SetEnvironmentVariable(key, value);

            return this;
        }

        private void ProcessMvc()
        {
            var mvcBuilder = Services.AddRouting().AddMvc();

            mvcBuilder.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));

            TypeFinder.FindTypes(IsGlobalFilter)
                .Foreach(filter => mvcBuilder.AddMvcOptions(o => o.Filters.Add(filter)));
        }

        private void ProcessInternalRecorders()
        {
            TypeFinder[] finders = Modules.Select(module => new TypeFinder(module.GetType().Assembly)).ToArray();
            new ResolvingAttributeServiceRecorder(finders).Process(Services);
        }

        private bool IsGlobalFilter(TypeInfo type)
        {
            return type.ImplementedInterfaces.Contains(typeof(IGlobalFilter));
        }
    }
}