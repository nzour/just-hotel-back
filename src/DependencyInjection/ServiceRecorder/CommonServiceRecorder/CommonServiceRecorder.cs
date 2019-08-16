using app.Aspect;
using FluentNHibernate.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace app.DependencyInjection.ServiceRecorder.CommonServiceRecorder
{
    public class CommonServiceRecorder : AbstractServiceRecorder
    {
        protected override void Execute(IServiceCollection services)
        {
            RecordServices(services);
            RecordExceptionMiddleware(services);
        }

        private void RecordServices(IServiceCollection services)
        {
            FindTypes(t => (bool) t.Namespace?.Contains("app.Common.Services") && !t.IsInterface)
                .Each(s => services.AddTransient(s));
        }

        private void RecordExceptionMiddleware(IServiceCollection services)
        {
            services.AddTransient<HandledExceptionMiddleware>();
        }
    }
}