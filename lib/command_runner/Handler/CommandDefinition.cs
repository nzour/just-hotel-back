using System;
using command_runner.Abstraction;

namespace command_runner.Handler
{
    public class CommandDefinition
    {
        public string Name { get; set; }
        public AbstractCommand AssembledCommand { get; set; }

        public CommandDefinition(string name, AbstractCommand assembledCommand)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            AssembledCommand = assembledCommand;
        }
    }
}