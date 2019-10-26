using System.Reflection;
using command_runner.Handler.Console;
using Microsoft.Extensions.DependencyInjection;

namespace CommandRunner
{
    public class MainRunner
    {
        public MainRunner(Assembly cliScope, IServiceCollection services)
        {
            CliScope = cliScope;
            Services = services ?? new ServiceCollection();
        }

        public Assembly CliScope { get; }
        public IServiceCollection Services { get; }

        public void Execute(string? defaultCommand = null)
        {
            var consoleManager = new ConsoleManager(CliScope, Services);

            if (0 != defaultCommand?.Length)
                consoleManager.InvokeWithoutStart(defaultCommand!);
            else
                consoleManager.Start();
        }
    }
}