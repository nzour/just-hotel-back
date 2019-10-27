using System.IO;
using Application;
using CommandRunner;
using Infrastructure;
using Root;
using _Kernel = Kernel.Kernel;

namespace cli
{
    public static class CliProgram
    {
        public static void Main(string[] args)
        {
            var envFile = $"{Directory.GetCurrentDirectory()}/publish/environment.json";

            var kernel = new _Kernel(typeof(Startup).Assembly);

            kernel
                .LoadModules(new ApplicationModule(), new InfrastructureModule())
                .LoadEnvironmentVariables(envFile)
                .Boot();

            var commandRunner = new MainRunner(typeof(CliProgram).Assembly, kernel.Services);
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}