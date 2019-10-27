using Domain;
using Infrastructure.Common.DiRecorder;
using Kernel;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class InfrastructureModule : IModule
    {
        public void Boot(IServiceCollection services)
        {
            var finder = new TypeFinder(GetType().Assembly);
            var domainFinder = new TypeFinder(typeof(AbstractEntity).Assembly);

            new SessionFactoryRecorder(finder).Process(services);
            new RepositoryRecorder(domainFinder, finder).Process(services);
            new JwtServiceRecorder().Process(services);
        }
    }
}