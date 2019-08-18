using System.Collections.Generic;

namespace command_runner.Handler.Console
{
    public class ConsoleArgs
    {
        public string Key { get; }
        public string Action { get; }
        public string[] Arguments { get; }

        public ConsoleArgs(string consoleLine)
        {
            var matches = consoleLine.Split(" ".ToCharArray());

            if (matches.Length == 0)
            {
                throw new System.Exception("Invalid string");
            }

            var args = new List<string>();
            
            if (matches.Length > 2)
            {
                for (var i = 2; i < matches.Length; i++)
                {
                    args.Add(matches[i]);
                }
            }

            Key = matches[0];
            Action = matches.Length > 1 ? matches[1] : null;
            Arguments = args.ToArray();
        }
    }
}