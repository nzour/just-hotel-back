using App.Application.Abstraction;
using App.Application.CQS.Auth.Exception;
using App.Application.CQS.Auth.Input;
using App.Application.CQS.Auth.Output;
using App.Domain.UserEntity;

namespace App.Application.CQS.Auth.Command
{
    public class SignUpCommand
    {
        private IUserRepository UserRepository { get; }
        private IPasswordEncoder PasswordEncoder { get; }
        private SignInCommand SignInCommand { get; }

        public SignUpCommand(IUserRepository userRepository, IPasswordEncoder passwordEncoder, SignInCommand signInCommand)
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
            SignInCommand = signInCommand;
        }

        public SignInOutput Execute(SignUpInput input)
        {
            AssertLoginIsNotBusy(input.Login);

            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = new User(input.FirstName, input.LastName, input.Login, encryptedPassword, UserRole.Client);

            UserRepository.CreateUser(user);

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