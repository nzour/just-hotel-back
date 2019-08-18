using command_runner.Abstraction;
using command_runner.Handler;

namespace cli.Commands
{
    public class MigrateAllCommand : AbstractCommand
    {
        public override void Execute(ArgumentProvider provider)
        {
            // command migrations:migrate
            // todo: implement
        }

        public override string GetName()
        {
            return "migrations:migrate";
        }
    }
}