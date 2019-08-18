using System;
using System.Collections.Generic;
using System.Linq;

namespace command_runner.Handler.Console
{
    public class ConsoleManager
    {
        private CommandManager Manager { get; }

        private Dictionary<string, string> DefaultScripts { get; } = new Dictionary<string, string>();

        private Dictionary<string, Action<ConsoleArgs>> Scripts { get; } =
            new Dictionary<string, Action<ConsoleArgs>>();

        public ConsoleManager(CommandManager manager)
        {
            Manager = manager;

            Startup();
        }

        public void StartListening()
        {
            Invoke(new ConsoleArgs("help"));

            try
            {
                var consoleLine = System.Console.ReadLine();
                Invoke(new ConsoleArgs(consoleLine));
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("------ | Error Occurred | ------");
                System.Console.WriteLine(e.Message);
            }
        }

        private void Startup()
        {
            DefaultScripts.Add("help", "Помощь");
            DefaultScripts.Add("command <CommandName> [Arguments...]", "Запуск команды");
            DefaultScripts.Add("commands", "Список зарегистрированных команд");

            var commandList = string.Join(", ", Manager.Commands.Select(c => c.Name));

            Scripts.Add("help", args => DisplayDefaultScripts());
            Scripts.Add("command", args => Manager.RunCommand(args.Action, args.Arguments));
            Scripts.Add("commands", args => System.Console.WriteLine($"Зарегистрированные команды: {commandList}"));
        }

        private void DisplayDefaultScripts()
        {
            foreach (var script in DefaultScripts)
            {
                System.Console.WriteLine($"Комманда {script.Key} - {script.Value}");
            }
            
            System.Console.WriteLine();
        }

        public void InvokeDefault(string defaultLine)
        {
            Invoke(new ConsoleArgs(defaultLine));
        }
        
        private void Invoke(ConsoleArgs args)
        {
            var found = Scripts.TryGetValue(args.Key, out var action);

            if (!found)
            {
                throw new System.Exception($"Command '{args.Key}' was not found");
            }

            action.Invoke(args);
        }
    }
}