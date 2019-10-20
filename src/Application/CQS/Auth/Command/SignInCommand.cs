using App.Application.Abstraction;
using App.Application.CQS.Auth.Exception;
using App.Application.CQS.Auth.Input;
using App.Application.CQS.Auth.Output;
using App.Domain.UserEntity;

namespace App.Application.CQS.Auth.Command
{
    public class SignInCommand
    {
        public IUserRepository UserRepository { get; }
        public IJwtTokenService TokenService { get; }
        public IPasswordEncoder PasswordEncoder { get; }

        public SignInCommand(
            IUserRepository userRepository,
            IJwtTokenService tokenService,
            IPasswordEncoder passwordEncoder
        )
        {
            UserRepository = userRepository;
            TokenService = tokenService;
            PasswordEncoder = passwordEncoder;
        }

        public SignInOutput Execute(SignInInput input)
        {
            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = UserRepository.FindUserWithLoginAndPassword(input.Login, encryptedPassword);

            return null != user
                ? new SignInOutput(user.Id, TokenService.CreateToken(user))
                : throw UserNotFound.InvalidCredentials();
        }
    }
}