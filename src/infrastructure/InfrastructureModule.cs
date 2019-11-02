using Domain;
using Infrastructure.Common.DiRecorder;
using Kernel;
using Kernel.Extension;
using Kernel.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class InfrastructureModule : IModule
    {
        public void Boot(IServiceCollection services)
        {
            var infrastructureFinder = new TypeFinder(GetType().Assembly);
            var domainFinder = new TypeFinder(typeof(AbstractEntity).Assembly);

            var configuration = services.GetService<IConfiguration>()!;

            new SessionFactoryRecorder(configuration, domainFinder, infrastructureFinder).Process(services);
            new RepositoryRecorder(domainFinder, infrastructureFinder).Process(services);
            new JwtServiceRecorder(configuration).Process(services);
        }
    }
}