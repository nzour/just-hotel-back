using app.Application.Abstraction;
using app.Application.CQS.Token.Exception;
using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Domain.User;

namespace app.Application.CQS.Token.Query
{
    public class ObtainTokenQuery
    {
        public IUserRepository UserRepository { get; }
        public IJwtTokenService TokenService { get; }
        public IPasswordEncoder PasswordEncoder { get; }

        public ObtainTokenQuery(
            IUserRepository userRepository,
            IJwtTokenService tokenService,
            IPasswordEncoder passwordEncoder
        )
        {
            UserRepository = userRepository;
            TokenService = tokenService;
            PasswordEncoder = passwordEncoder;
        }

        public TokenOutput Execute(TokenInput input)
        {
            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = UserRepository.FindUserWithLoginAndPassword(input.Login, encryptedPassword);

            return null != user
                ? new TokenOutput(user.Id, TokenService.CreateToken(user))
                : throw UserNotFound.InvalidCredentials();
        }
    }
}