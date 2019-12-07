using System.Collections.Generic;
using System.Linq;

namespace CommandRunner.Handler.Exception
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

        public static CommandArgumentException NotEquals<T>(IEnumerable<T> expected, T actual)
        {
            var expectedString = string.Join(", ", expected.Select(item => $"'{item}'"));
            return new CommandArgumentException($@"Expected values: {expectedString}. '{actual}' was specified.");
        }
    }
}