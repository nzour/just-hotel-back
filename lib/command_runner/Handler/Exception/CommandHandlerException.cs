using System;
using System.Collections.Generic;
using System.Reflection;

namespace CommandRunner.Handler.Exception
{
    public class CommandHandlerException : System.Exception
    {
        public CommandHandlerException(string message) : base(message)
        {
        }

        public static CommandHandlerException DuplicatedNames(IEnumerable<string> duplicates)
        {
            return new CommandHandlerException(
                $"There are duplicated command names was found: {string.Join(", ", duplicates)}.");
        }

        public static CommandHandlerException NotFound(string name)
        {
            return new CommandHandlerException($"Command with name {name} was not found.");
        }

        public static CommandHandlerException CommandArgumentsCount(int expected, int actual)
        {
            return new CommandHandlerException($"Expected command arguments {expected}, {actual} was specified.");
        }

        public static CommandHandlerException CommandArgumentPropertyIsNotScalar(Type argument, PropertyInfo property)
        {
            return new CommandHandlerException(
                $"Command argument {argument.Name} contains non scalar property {property.Name}");
        }
    }
}