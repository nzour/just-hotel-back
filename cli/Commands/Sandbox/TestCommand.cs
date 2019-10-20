using app.Application.CQS.Auth.Command;
using app.Application.CQS.Auth.Input;
using app.Domain.User;
using command_runner.Abstraction;
using command_runner.Handler;

namespace cli.Commands.Sandbox
{
    public class TestCommand : AbstractCommand
    {
        public SignUpCommand Command { get; }
        public IUserRepository UserRepository { get; }

        public TestCommand(SignUpCommand command, IUserRepository userRepository)
        {
            Command = command;
            UserRepository = userRepository;
        }

        public override void Execute(ArgumentProvider provider)
        {
            var input = new SignUpInput
            {
                Login = provider.NextAsString(),
                FirstName = provider.NextAsString(),
                LastName = provider.NextAsString(),
                Password = provider.NextAsString()
            };

            Command.Execute(input);

            var isBusy = UserRepository.IsLoginBusy(input.Login);
        }

        public override string GetName()
        {
            return "app:test";
        }
    }
}