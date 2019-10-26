using Application.Abstraction;
using Application.CQS.Auth.Exception;
using Application.CQS.Auth.Input;
using Application.CQS.Auth.Output;
using Domain.UserEntity;

namespace Application.CQS.Auth.Command
{
    public class SignInCommand
    {
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

        public IUserRepository UserRepository { get; }
        public IJwtTokenService TokenService { get; }
        public IPasswordEncoder PasswordEncoder { get; }

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