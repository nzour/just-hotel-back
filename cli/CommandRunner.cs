using System;
using app;
using app.Domain.Entity.User;
using app.Infrastructure.NHibernate.Repository;
using cli.CommandHandler;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using kernel;
using kernel.Extensions;

namespace cli
{
    internal static class CommandRunner
    {
        public static readonly Kernel Kernel = new Kernel(typeof(Startup).Assembly);
        public static readonly CommandManager CommandManager = new CommandManager(Kernel, typeof(CommandRunner).Assembly);

        public static void Main(string[] args)
        {
            Kernel.Boot();

            var line = Console.ReadLine();
            
            CommandManager.RunCommand("app:test", line);
        }
    }
}