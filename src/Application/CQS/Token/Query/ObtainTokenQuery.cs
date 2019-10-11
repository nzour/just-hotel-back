using app.Application.Abstraction;
using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Domain.User;

namespace app.Application.CQS.Token.Query
{
    public class ObtainTokenQuery
    {
        public IUserRepository UserRepository { get; }
        public IJwtTokenService TokenService { get; }

        public ObtainTokenQuery(IUserRepository userRepository, IJwtTokenService tokenService)
        {
            UserRepository = userRepository;
            TokenService = tokenService;
        }

        public TokenOutput Execute(TokenInput input)
        {
//            var user = UserRepository.FindUserWithLogin(input.Login);

            var user = new User("foobar", "barfoo", "barbaz");

            return new TokenOutput
            {
                UserId = user.Id,
                Roles = user.Roles,
                Token = TokenService.CreateToken(user),
            };
        }
    }
}