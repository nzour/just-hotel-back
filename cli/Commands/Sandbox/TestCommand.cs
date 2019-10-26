using command_runner.Abstraction;
using command_runner.Handler;

namespace Cli.Commands.Sandbox
{
    public class TestCommand : AbstractCommand
    {
        public override void Execute(ArgumentProvider provider)
        {
//            var input = new SignUpInput
//            {
//                Login = "zaur_client",
//                Password = "zaur_client",
//                FirstName = "Заур",
//                LastName = "Надиралиев"
//            };
//
//            Command.Execute(input);
        }

        public override string GetName()
        {
            return "app:test";
        }
    }
}