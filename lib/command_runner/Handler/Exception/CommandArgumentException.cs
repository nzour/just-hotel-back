using System;

namespace command_runner.CommandHandler.Exception
{
    public class CommandArgumentException : System.Exception
    {
        public CommandArgumentException(string message) : base(message)
        {
            
        }

        public static CommandArgumentException NotFound(int position)
        {
            return new CommandArgumentException($@"There are no ${++position} argument was specified.");
        }
    }
}