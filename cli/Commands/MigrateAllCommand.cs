using command_runner.Abstraction;
using command_runner.Handler;
using FluentMigrator.Runner;

namespace cli.Commands
{
    public class MigrateAllCommand : AbstractCommand
    {
        public IMigrationRunner Runner { get; }

        public MigrateAllCommand(IMigrationRunner runner)
        {
            Runner = runner;
        }

        public override void Execute(ArgumentProvider provider)
        {
            Runner.MigrateUp();
        }

        public override string GetName()
        {
            return "migrations:migrate";
        }

        public override string GetDescription()
        {
            return "Накатит все миграции, которые не были накатаны ранее, на текущую архитектуру БД.";
        }
    }
}