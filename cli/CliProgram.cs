using System.IO;
using System.Reflection;
using app;
using command_runner;
using kernel;
using Microsoft.Extensions.DependencyInjection;

namespace cli
{
    public static class CliProgram
    {
        public static void Main(string[] args)
        {
            var kernel = new Kernel(typeof(Startup).Assembly);
            
            kernel.LoadEnvironment(Directory.GetCurrentDirectory() + "/publish/environment.json");
            kernel.Boot();
            
            var commandRunner = new CommandRunner(typeof(CliProgram).Assembly, kernel.Services);
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}