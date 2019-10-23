using App.Application.CQS.Auth.Command;
using App.Application.CQS.Auth.Input;
using command_runner.Abstraction;
using command_runner.Handler;

namespace Cli.Commands.Sandbox
{
    public class TestCommand : AbstractCommand
    {
        public SignUpCommand Command { get; }

        public TestCommand(SignUpCommand command)
        {
            Command = command;
        }

        public override void Execute(ArgumentProvider provider)
        {
            var input = new SignUpInput
            {
                Login = "zaur_client",
                Password = "zaur_client",
                FirstName = "Заур",
                LastName = "Надиралиев"
            };

            Command.Execute(input);
        }

        public override string GetName()
        {
            return "app:test";
        }
    }
}