using System.Reflection;
using command_runner.Handler.Console;
using Microsoft.Extensions.DependencyInjection;

namespace command_runner
{
    public class CommandRunner
    {
        public Assembly CliScope { get; }
        public IServiceCollection Services { get; }


        public CommandRunner(Assembly cliScope, IServiceCollection services)
        {
            CliScope = cliScope;
            Services = services ?? new ServiceCollection();
        }

        public void Execute(string defaultCommand = null)
        {
            var consoleManager = new ConsoleManager(CliScope, Services);

            if (0 == defaultCommand?.Length)
            {
                consoleManager.InvokeWithoutStart(defaultCommand);
            }
            else
            {
                consoleManager.Start();
            }
        }
    }
}