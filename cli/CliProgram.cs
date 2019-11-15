using Application;
using CommandRunner;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Root;

namespace cli
{
    internal static class CliProgram
    {
        public static void Main(string[] args)
        {
            var configuration = Program.CreateConfiguration();
            var services = new ServiceCollection();

            services.AddScoped(_ => Program.CreateConfiguration());

            services
                .AddInfrastructure(configuration)
                .AddApplication();

            var commandRunner = new MainRunner(typeof(CliProgram).Assembly, services);
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}