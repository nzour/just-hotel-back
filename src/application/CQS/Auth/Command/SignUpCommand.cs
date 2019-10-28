using Application.Abstraction;
using Application.CQS.Auth.Exception;
using Application.CQS.Auth.Input;
using Application.CQS.Auth.Output;
using Domain.User;
using Domain.UserEntity;

namespace Application.CQS.Auth.Command
{
    public class SignUpCommand
    {
        public SignUpCommand(
            IUserRepository userRepository, IPasswordEncoder passwordEncoder, SignInCommand signInCommand
        )
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
            SignInCommand = signInCommand;
        }

        private IUserRepository UserRepository { get; }
        private IPasswordEncoder PasswordEncoder { get; }
        private SignInCommand SignInCommand { get; }

        public SignInOutput Execute(SignUpInput input)
        {
            AssertLoginIsNotBusy(input.Login);

            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = new UserEntity(input.FirstName, input.LastName, input.Login, encryptedPassword, UserRole.Client);

            UserRepository.CreateUser(user);

            return SignInCommand.Execute(new SignInInput(user.Login, user.Password));
        }

        private void AssertLoginIsNotBusy(string login)
        {
            if (UserRepository.IsLoginBusy(login)) throw new UserLoginIsBusyException(login);
        }
    }
}