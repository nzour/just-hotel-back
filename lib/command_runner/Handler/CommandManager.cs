using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using command_runner.Abstraction;
using command_runner.Handler.Exception;
using common.Extensions;
using FluentNHibernate.Conventions;
using kernel;
using kernel.Extensions;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace command_runner.Handler
{
    public class CommandManager
    {
        public Kernel Kernel { get; }
        private TypeFinder TypeFinder { get; }
        public IEnumerable<CommandDefinition> Commands { get; private set; }

        internal CommandManager(Kernel kernel, Assembly cliScope)
        {
            Kernel = kernel;
            TypeFinder = new TypeFinder(cliScope);
            RegisterCommands();
        }

        public bool HasCommand(string name)
        {
            return Commands.Select(command => command.Name).Contains(name);
        }

        public void RunCommand(string name, string[] arguments)
        {
            var foundCommand = Commands.First(command => command.Name == name);
            
            foundCommand.IfIsNull(() => throw CommandHandlerException.NotFound(name));

            foundCommand.AssembledCommand.Execute(new ArgumentProvider(arguments));
        }
        
        private void RegisterCommands()
        {
            var commandTypes = TypeFinder
                .FindTypes(t => t.IsSubclassOf(typeof(AbstractCommand)) && !t.IsAbstract);

            commandTypes.Foreach(pureCommand => Kernel.Services.AddTransient(pureCommand));

            Commands = commandTypes.Map(type => Kernel.Services.GetService<AbstractCommand>(type))
                .Select(command => new CommandDefinition(command.GetName(), command));

            var duplicates = Commands.FindDuplicates(command => command.Name);

            if (duplicates.IsNotEmpty())
            {
                throw CommandHandlerException.DuplicatedNames(duplicates);
            }
        }
    }
}