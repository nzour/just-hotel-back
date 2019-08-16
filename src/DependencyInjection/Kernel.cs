using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using app.Aspect.FilterAttribute;
using app.Common.Services.Migrator;
using app.DependencyInjection.ServiceRecorder;
using app.Infrastructure.NHibernate;
using FluentNHibernate.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace app.DependencyInjection
{
    /// <summary>
    ///  Core класс приложения, через него описываются все зависимости, которые необходимо выставить перед стартом приложения.
    /// </summary>
    public class Kernel : AbstractAssemblyAware
    {
        /// <summary>
        /// По дефолту, файл с переменными окружения.
        /// </summary>
        private const string EnvFile = "environment.json";

        public static void ProcessServices(IServiceCollection services)
        {
            RegisterEnvironment();
            ProcessServiceRecorders(services);
            NHibernateHelper.Boot();
            services.AddHttpContextAccessor();
            new Migrator(services).Execute();
        }

        public static void ProcessMvc(IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            RegisterGlobalFilters(mvcBuilder);
        }

        /// <summary>
        /// Запустит все AbstractServiceRecorder'ы, которые регистрируют сервисы DI.
        /// </summary>
        private static void ProcessServiceRecorders(IServiceCollection services)
        {
            FindTypes(t => t.IsSubclassOf(typeof(AbstractServiceRecorder)) && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t) as AbstractServiceRecorder)
                .Each(recorder => recorder.Process(services));
        }

        /// <summary>
        /// Регистрирует все перменные окружения из файла environment.json (по-умолчанию).
        /// </summary>
        private static void RegisterEnvironment()
        {
            StreamReader reader;

            try
            {
                reader = new StreamReader(EnvFile);
            }
            catch
            {
                reader = new StreamReader("publish/" + EnvFile);
            }

            using (reader)
            {
                var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());

                foreach (var (key, value) in json)
                {
                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }

        /// <summary>
        /// Добавит глобальные фильтры для контроллеров
        /// </summary>
        private static void RegisterGlobalFilters(IMvcBuilder mvcBuilder)
        {
            var globalFilters = FindTypes(t =>
                t.ImplementedInterfaces.Contains(typeof(IGlobalFilter)));

            foreach (var globalFilter in globalFilters)
            {
                mvcBuilder.AddMvcOptions(options => { options.Filters.Add(globalFilter); });
            }
        }
    }
}