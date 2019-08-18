using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Util;

namespace kernel.Abstraction
{
    public abstract class AbstractServiceRecorder
    {
        protected bool IsExecuted { get; private set; }

        public void Process(IServiceCollection services)
        {
            if (IsExecuted)
            {
                return;
            }

            // Сначала запускаем все service recorder'ы, от которых зависит текущий
            foreach (var dependency in GetDependencies())
            {
                dependency.Process(services);
            }
            
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