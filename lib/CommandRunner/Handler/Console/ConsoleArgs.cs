using System.Collections.Generic;
using System.Linq;

namespace CommandRunner.Handler.Console
{
    public class ConsoleArgs
    {
        public ConsoleArgs(string consoleLine)
        {
            IEnumerable<string> matches = consoleLine.Split(" ".ToCharArray());

            if (!matches.Any())
            {
                throw new System.Exception("Invalid string");
            }

            var args = new List<string>();
            var count = matches.Count();

            if (count > 2)
            {
                for (var i = 2; i < count; i++)
                {
                    args.Add(matches.ToArray()[i]);
                }
            }

            Key = matches.First();
            Action = count > 1 ? matches.ToArray()[1] : null;
            Arguments = args.ToArray();
        }

        public string Key { get; }
        public string? Action { get; }
        public string[] Arguments { get; }
    }
}