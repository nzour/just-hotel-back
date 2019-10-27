using Microsoft.Extensions.DependencyInjection;

namespace Kernel
{
    public interface IModule
    {
        void Boot(IServiceCollection services);
    }
}