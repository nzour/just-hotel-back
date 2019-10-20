using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using command_runner.Abstraction;
using command_runner.Handler.Exception;
using Microsoft.Extensions.DependencyInjection;

namespace command_runner.Handler
{
    public class CommandManager
    {
        public IServiceCollection Services { get; }
        private Assembly CliScope { get; }
        public IEnumerable<AbstractCommand?> Commands { get; private set; } = new List<AbstractCommand>();

        internal CommandManager(Assembly cliScope, IServiceCollection services)
        {
            CliScope = cliScope;
            Services = services;

            RegisterCommands();
        }

        public bool HasCommand(string name)
        {
            return null != Commands.First(c => c!.GetName() == name);
        }

        public void RunCommand(string name, string[] arguments)
        {
            GetCommand(name).Execute(new ArgumentProvider(arguments));
        }

        public string? GetCommandDescription(string name)
        {
            return GetCommand(name).GetDescription();
        }

        private void RegisterCommands()
        {
            var commandTypes = CliScope.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(AbstractCommand)) && !t.IsAbstract);

            foreach (var type in commandTypes)
            {
                Services.AddTransient(type);
            }

            Commands = commandTypes
                .Select(type => Services.BuildServiceProvider().GetService(type) as AbstractCommand)
                .Where(c => null != c);

            var duplicates = FindDuplicates();

            if (0 != duplicates.Count())
            {
                throw CommandHandlerException.DuplicatedNames(duplicates.Select(c => c.GetName()));
            }
        }

        private IEnumerable<AbstractCommand> FindDuplicates()
        {
            var count = Commands.Count();
            var array = Commands.ToArray();

            var result = new List<AbstractCommand>();

            if (1 == count)
            {
                return result;
            }

            for (var i = 1; i < count; i++)
            {
                if (Equals(array[i]!.GetName(), array[i - 1]!.GetName()))
                {
                    result.Add(array[i]);
                }
            }

            return result;
        }

        private AbstractCommand GetCommand(string name)
        {
            var foundCommand = Commands.First(c => c!.GetName() == name);

            if (null == foundCommand)
            {
                throw CommandHandlerException.NotFound(name);
            }

            return foundCommand;
        }
    }
}