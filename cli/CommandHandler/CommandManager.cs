using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cli.Abstraction;
using cli.CommandHandler.Exception;
using common.Extensions;
using FluentNHibernate.Conventions;
using kernel;
using kernel.Extensions;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace cli.CommandHandler
{
    internal class CommandManager
    {
        public Kernel Kernel { get; }
        private TypeFinder TypeFinder { get; }
        public IEnumerable<CommandDefinition> Commands { get; private set; }

        internal CommandManager(Kernel kernel, Assembly assembly)
        {
            Kernel = kernel;
            TypeFinder = new TypeFinder(assembly);
            RegisterCommands();
        }

        public bool HasCommand(string name)
        {
            return Commands.Select(command => command.Name).Contains(name);
        }

        public void RunCommand(string name, string arguments)
        {
            var foundCommand = Commands.First(command => command.Name == name);
            
            foundCommand.IfIsNull(() => throw CommandHandlerException.NotFound(name));

            var argument = CompileArgument(foundCommand.Argument, arguments);
            
            foundCommand.AssembledCommand.Execute(argument);
        }
        
        private void RegisterCommands()
        {
            var commandTypes = TypeFinder
                .FindTypes(t => t.IsSubclassOf(typeof(AbstractCommand)) && !t.IsAbstract);

            commandTypes.Foreach(pureCommand => Kernel.Services.AddTransient(pureCommand));

            Commands = commandTypes.Map(type => Kernel.Services.GetService<AbstractCommand>(type))
                .Select(command => new CommandDefinition(command.GetName(), command, command.Argument));

            var duplicates = Commands.FindDuplicates(command => command.Name);

            if (duplicates.IsNotEmpty())
            {
                throw CommandHandlerException.DuplicatedNames(duplicates);
            }
        }

        private ICommandArgument CompileArgument(ParameterInfo parameter, string arguments)
        {
            if (parameter.ParameterType.IsInterface || parameter.GetType() == typeof(NullCommandArgument))
            {
                return new NullCommandArgument();
            }
            
            var args = arguments.Split(" ");

            var argument = (ICommandArgument) Activator.CreateInstance(parameter.ParameterType);
            
            var properties = parameter.ParameterType.GetProperties().ToArray();


            if (properties.Length != args.Length)
            {
                throw CommandHandlerException.CommandArgumentsCount(properties.Length, args.Length);
            }

            return FillProperties(argument, args, properties);
        }

        private ICommandArgument FillProperties(ICommandArgument argument, string[] args, PropertyInfo[] properties)
        {
            for (var i = 0; i < properties.Length; i++)
            {
                var currentProp = properties[i];
                
                if (currentProp.PropertyType.IsClass && currentProp.PropertyType.IsGenericType)
                {
                    throw CommandHandlerException.CommandArgumentPropertyIsNotScalar(argument.GetType(), currentProp);
                }

                var value = Convert.ChangeType(args[i], currentProp.PropertyType);
                
                currentProp.SetValue(argument, value);
            }

            return argument;
        }
    }
}