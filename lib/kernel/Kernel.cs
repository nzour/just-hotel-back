using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using common.Extensions;
using kernel.Abstraction;
using kernel.Extension;
using kernel.Internal;
using kernel.Service;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace kernel
{
    public class Kernel
    {
        public Assembly ApplicationScope { get; }
        public IServiceCollection Services { get; }
        public TypeFinder TypeFinder { get; }

        public Kernel(Assembly applicationScope, IServiceCollection? services = null)
        {
            ApplicationScope = applicationScope;
            Services = services ?? new ServiceCollection();
            TypeFinder = new TypeFinder(ApplicationScope);

            Services.AddSingleton(TypeFinder);
        }

        public void Boot()
        {
            ProcessInternalRecorders();
            ProcessRecorders();
            ProcessMvc();

            Services.AddHttpContextAccessor();
        }

        public void LoadEnvironmentVariables(string envFilename)
        {
            if (!File.Exists(envFilename))
            {
                throw new FileNotFoundException(
                    $"Unable to read environment file. File with name {envFilename} was not found.");
            }

            File.ReadAllText(envFilename);

            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(envFilename));

            foreach (var (key, value) in json)
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }

        private void ProcessRecorders()
        {
            TypeFinder.FindTypes(t => t.IsSubclassOf(typeof(AbstractServiceRecorder)) && !t.IsAbstract)
                .Foreach(recorder =>
                {
                    Services.AddTransient(recorder)
                        .GetService<AbstractServiceRecorder>(recorder)
                        .Process(Services);
                });
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
            Services.AddTransient(typeof(ResolvingAttributeServiceRecorder));
            Services.GetService<ResolvingAttributeServiceRecorder>()!.Process(Services);
        }

        private bool IsGlobalFilter(TypeInfo type)
        {
            return type.ImplementedInterfaces.Contains(typeof(IGlobalFilter));
        }
    }
}