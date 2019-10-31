using Application.Abstraction;
using Application.CQS.Auth.Exception;
using Application.CQS.Auth.Input;
using Application.CQS.Auth.Output;
using Domain.User;

namespace Application.CQS.Auth.Command
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
                ? new SignInOutput(user, TokenService.CreateToken(user))
                : throw UserNotFound.InvalidCredentials();
        }
    }
}