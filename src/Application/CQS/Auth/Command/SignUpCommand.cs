using app.Application.Abstraction;
using app.Application.CQS.Auth.Exception;
using app.Application.CQS.Auth.Input;
using app.Application.CQS.Auth.Output;
using app.Domain.User;

namespace app.Application.CQS.Auth.Command
{
    public class SignUpCommand
    {
        public IUserRepository UserRepository { get; }
        public IPasswordEncoder PasswordEncoder { get; }
        public SignInCommand SignInCommand { get; }

        public SignUpCommand(IUserRepository userRepository, IPasswordEncoder passwordEncoder,
            SignInCommand signInCommand)
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
            SignInCommand = signInCommand;
        }

        public SignInOutput Execute(SignUpInput input)
        {
            AssertLoginIsNotBusy(input.Login);

            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = new User(input.FirstName, input.LastName, input.Login, encryptedPassword);

            UserRepository.CreateUserAsync(user);

            return SignInCommand.Execute(new SignInInput(user.Login, user.Password));
        }

        private void AssertLoginIsNotBusy(string login)
        {
            if (UserRepository.IsLoginBusy(login))
            {
                throw new UserLoginIsBusyException(login);
            }
        }
    }
}