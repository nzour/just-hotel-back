using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Common;
using app.Common.Services.Jwt;
using app.Domain.Entity.User;
using MyToken = app.Domain.Entity.Token.Token;

namespace app.Application.CQS.Token.Command
{
    public class ReceiveTokenCommand
    {
        public IUserRepository UserRepository { get; }
        public JwtTokenManager TokenManager { get; }

        public ReceiveTokenCommand(IUserRepository userRepository, JwtTokenManager tokenManager)
        {
            UserRepository = userRepository;
            TokenManager = tokenManager;
        }

        public TokenOutput Execute(TokenInput input)
        {
            var user = UserRepository.FindUserWithLogin(input.Login);

            if (null == user || EncodeHandler.EncodePassword(input.Password) != user.Password)
            {
                throw UserException.NotFound(input.Login);
            }

            return new TokenOutput(TokenManager.CreateAccessTokenForUser(user));
        }
    }
}