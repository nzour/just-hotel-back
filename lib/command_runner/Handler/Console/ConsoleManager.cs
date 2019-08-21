using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace command_runner.Handler.Console
{
    public class ConsoleManager
    {
        private CommandManager CommandManager { get; }

        private Dictionary<string, string> Messages { get; } = new Dictionary<string, string>();

        private Dictionary<string, Action<ConsoleArgs>> Scripts { get; } =
            new Dictionary<string, Action<ConsoleArgs>>();

        public ConsoleManager(Assembly cliScope, IServiceCollection services)
        {
            CommandManager = new CommandManager(cliScope, services);
            Startup();
        }

        public void Start()
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
        
        public void InvokeWithoutStart(string defaultLine)
        {
            Invoke(new ConsoleArgs(defaultLine));
        }

        private void Startup()
        {
            Messages.Add("help", "Помощь");
            Messages.Add("command <CommandName> [Arguments...]", "Запуск команды");
            Messages.Add("commands", "Список зарегистрированных команд");
            Messages.Add("info <CommandName>", "Посмотреть информацию о команде, если команда её имеет.");

            var commandList = string.Join(", ", CommandManager.Commands.Select(c => c.GetName()));

            Scripts.Add("help", args => DisplayDefaultScripts());
            Scripts.Add("command", args => CommandManager.RunCommand(args.Action, args.Arguments));
            Scripts.Add("commands", args => System.Console.WriteLine($"Зарегистрированные команды: {commandList}"));
            Scripts.Add("info", args => System.Console.Write(CommandManager.GetCommandDescription(args.Action)));
        }

        private void DisplayDefaultScripts()
        {
            foreach (var script in Messages)
            {
                System.Console.WriteLine($"Комманда {script.Key} - {script.Value}");
            }
            
            System.Console.WriteLine();
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