using System.Collections.Generic;
using FluentNHibernate.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder
{
    public abstract class AbstractServiceRecorder : AbstractAssemblyAware
    {
        protected bool IsExecuted { get; private set; }

        public void Process(IServiceCollection services)
        {
            if (IsExecuted)
            {
                return;
            }
            
            // Сначала запускаем все service recorder'ы, от которые зависит текущий
            GetDependencies().Each(dependency => dependency.Process(services));
            Execute(services);

            IsExecuted = true;
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