using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder
{
    /// <summary>
    /// Все реализации интерфейса, будут запущены на этапе запуска приложения и должны регистрировать сервисы для DI
    /// </summary>
    public interface IServiceRecorder
    {
        /// <summary>
        /// Регистрирует необходимые сервисы
        /// </summary>
        void Process(IServiceCollection services);
    }
}