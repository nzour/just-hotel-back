using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using common.Extensions;
using kernel.Abstraction;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace kernel
{
    public class Kernel
    {
        public Assembly ApplicationScope { get; }
        public IServiceCollection Services { get; }
        public TypeFinder TypeFinder { get; }

        public Kernel(Assembly applicationScope, IServiceCollection services = null)
        {
            ApplicationScope = applicationScope;
            Services = services ?? new ServiceCollection();
            TypeFinder = new TypeFinder(ApplicationScope);

            Services.AddSingleton(TypeFinder);
        }

        public void Boot()
        {
            ProcessRecorders();
            ProcessGlobalFilters();

            Services.AddHttpContextAccessor();
        }

        public void LoadEnvironmentVariables(string envFilename)
        {
            if (!File.Exists(envFilename))
            {
                throw new FileNotFoundException($"Unable to read environment file. File with name {envFilename} was not found.");
            }

            File.ReadAllText(envFilename);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(envFilename));

            foreach (var pair in json)
            {
                Environment.SetEnvironmentVariable(pair.Key, pair.Value);
            }
        }

        private void ProcessRecorders()
        {
            TypeFinder.FindTypes(t => t.IsSubclassOf(typeof(AbstractServiceRecorder)) && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t) as AbstractServiceRecorder)
                .Foreach(recorder => recorder.Process(Services));
        }

        private void ProcessGlobalFilters()
        {
            var mvcCoreBuilder = Services.AddMvcCore();

            TypeFinder.FindTypes(t => t.ImplementedInterfaces.Contains(typeof(IGlobalFilter)))
                .Foreach(filter => mvcCoreBuilder.AddMvcOptions(o => o.Filters.Add(filter)));
        }
    }
}