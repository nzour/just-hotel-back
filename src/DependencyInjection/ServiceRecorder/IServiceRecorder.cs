using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder
{
    public interface IServiceRecorder
    {
        /// <summary>
        /// Регистрирует необходимые сервисы
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        void Process(IServiceCollection services);
    }
}