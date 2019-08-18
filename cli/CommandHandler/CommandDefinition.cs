using System;
using System.Reflection;
using cli.Abstraction;

namespace cli.CommandHandler
{
    public class CommandDefinition
    {
        public string Name { get; set; }
        public AbstractCommand AssembledCommand { get; set; }
        public ParameterInfo Argument { get; set; }

        public CommandDefinition(string name, AbstractCommand assembledCommand, ParameterInfo argument)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AssembledCommand = assembledCommand;
            Argument = argument;
        }
    }
}