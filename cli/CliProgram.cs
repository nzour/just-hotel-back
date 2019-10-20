using System.IO;
using App;
using CommandRunner;
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
                .LoadEnvironmentVariables(envFile)
                .Boot();

            var commandRunner = new MainRunner(typeof(CliProgram).Assembly, kernel.Services);
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}