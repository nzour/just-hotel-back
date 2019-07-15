using System;
using System.Linq;
using app.DependencyInjection.ServiceRecorder;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection
{
    /// <summary>
    ///  Core класс приложения, через него описываются все зависимости, которые необходимо выставить перед стартом приложения.
    /// </summary>
    public class Kernel : AbstractAssemblyAware
    {
        public static void Boot(IServiceCollection services)
        {
            RecordServices(services);
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
    }
}