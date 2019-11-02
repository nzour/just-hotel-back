using Application;
using CommandRunner;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Root;
using _Kernel = Kernel.Kernel;

namespace cli
{
    public static class CliProgram
    {
        public static void Main(string[] args)
        {
            var kernel = new _Kernel(typeof(Startup).Assembly);

            kernel.Services.AddScoped(_ => Program.CreateConfiguration());

            kernel
                .LoadModules(new ApplicationModule(), new InfrastructureModule())
                .Boot();

            var commandRunner = new MainRunner(typeof(CliProgram).Assembly, kernel.Services);
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}