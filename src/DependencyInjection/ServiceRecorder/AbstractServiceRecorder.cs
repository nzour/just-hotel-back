using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder
{
    public abstract class AbstractServiceRecorder : AbstractAssemblyAware
    {
        public const string ProcessMethod = "Process";
        
        protected bool IsExecuted { get; private set; }

        public void Process(IServiceCollection services)
        {
            if (IsExecuted)
            {
                return;
            }
            
            ExecuteDependencies(services);
            Execute(services);

            IsExecuted = true;
        }

        /// <summary>
        /// Выполнение других Recorder'ов, от которых зависит текущий.
        /// </summary>
        private void ExecuteDependencies(IServiceCollection services)
        {
            foreach (var dependency in GetDependencies())
            {
                dependency.Process(services);
            }
        }

        /// <summary>
        /// Массив зависимых Recorder'ов, от которых зависит текущий.
        /// WARNING: аккуратнее с рекурсией.
        /// </summary>
        protected virtual IEnumerable<AbstractServiceRecorder> GetDependencies()
        {
            return new AbstractServiceRecorder[] { };
        }

        /// <summary>
        /// Описываем логику выполнения записей сервисов.
        /// </summary>
        protected abstract void Execute(IServiceCollection services);
    }
}