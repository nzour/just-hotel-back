using Application.CQS.Auth.Exception;
using Application.CQS.Auth.Input;
using Application.CQS.Auth.Output;
using Domain.User;
using Infrastructure.Services;

namespace Application.CQS.Auth.Command
{
    public class SignUpCommand
    {
        private IUserRepository UserRepository { get; }

        private IPasswordEncoder PasswordEncoder { get; }
        private IJwtTokenService TokenService { get; }

        public SignUpCommand(IUserRepository userRepository, IPasswordEncoder passwordEncoder, IJwtTokenService tokenService)
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
            TokenService = tokenService;
        }

        public SignInOutput Execute(SignUpInput input)
        {
            AssertLoginIsNotBusy(input.Login);

            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = new UserEntity(input.FirstName, input.LastName, input.Login, encryptedPassword, UserRole.Client);

            UserRepository.SaveAndFlush(user);

            return new SignInOutput(user, TokenService.CreateToken(user));
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