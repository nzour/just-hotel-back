using command_runner.Abstraction;
using command_runner.Handler;

namespace cli.Commands
{
    public class MigrateCommand : AbstractCommand

    {
        public override void Execute(ArgumentProvider provider)
        {
            // command migrations:migrate <version> [up|down]
            // todo: implement
        }

        public override string GetName()
        {
            return "migrations:execute";
        }
    }
}