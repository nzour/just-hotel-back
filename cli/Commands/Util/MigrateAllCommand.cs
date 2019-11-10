using CommandRunner.Abstraction;
using CommandRunner.Handler;
using FluentMigrator.Runner;

namespace Cli.Commands.Util
{
    public class MigrateAllCommand : AbstractCommand
    {
        public MigrateAllCommand(IMigrationRunner runner)
        {
            Runner = runner;
        }

        public IMigrationRunner Runner { get; }

        public override void Execute(ArgumentProvider provider)
        {
            Runner.MigrateUp();
        }

        public override string GetName()
        {
            return "migrations:migrate";
        }

        public override string? GetDescription()
        {
            return "Накатит все миграции, которые не были накатаны ранее, на текущую архитектуру БД.";
        }
    }
}