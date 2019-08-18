using command_runner.Abstraction;
using command_runner.Handler;

namespace cli.Commands.Sandbox
{
    public class TestCommand : AbstractCommand
    {
        public override void Execute(ArgumentProvider provider)
        {
        }

        public override string GetName()
        {
            return "app:test";
        }
    }
}