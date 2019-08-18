using System.Reflection;
using command_runner.CommandHandler;
using command_runner.Handler;
using command_runner.Handler.Console;
using kernel;

namespace command_runner
{
    public class CommandRunner
    {
        public Assembly CliScope { get; }
        public Kernel Kernel { get; }

        public CommandRunner(Kernel kernel, Assembly cliScope)
        {
            Kernel = kernel;
            CliScope = cliScope;
        }

        public void Execute(string defaultCommand = null)
        {
            var commandManager = new CommandManager(Kernel, CliScope);
            var consoleManager = new ConsoleManager(commandManager);

            if (null != defaultCommand)
            {
                consoleManager.InvokeDefault(defaultCommand);
            }
            else
            {
                consoleManager.StartListening();
            }
            
        }
    }
}