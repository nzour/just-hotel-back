using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using app.Common;
using app.Common.Services.Migrator;
using app.DependencyInjection.ServiceRecorder;
using app.Infrastructure.NHibernate;
using FluentMigrator.Runner;
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

        public static void Boot(IServiceCollection services)
        {
            RegisterEnvironment();
            RecordServices(services);
            NHibernateHelper.Boot();
            new Migrator(services).Execute();
        }

        /// <summary>
        /// Запустит все AbstractServiceRecorder'ы, которые регистрируют сервисы DI.
        /// </summary>
        private static void RecordServices(IServiceCollection services)
        {
            var recorders = GetAssembly()
                .DefinedTypes
                .Where(type => type.IsSubclassOf(typeof(AbstractServiceRecorder)) && !type.IsAbstract);

            foreach (var recorder in recorders)
            {
                recorder.GetMethod(AbstractServiceRecorder.ProcessMethod)
                    .Invoke(Activator.CreateInstance(recorder), new object[] { services });
            }
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
    }
}