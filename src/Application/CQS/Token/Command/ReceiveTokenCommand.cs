using System;
using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Common.Extensions;
using app.Domain.Entity.Token;
using MyToken = app.Domain.Entity.Token.Token;
using app.Domain.Entity.User;

namespace app.Application.CQS.Token.Command
{
    public class ReceiveTokenCommand
    {
        public IUserRepository UserRepository { get; }
        public ITokenRepository TokenRepository { get; }

        public ReceiveTokenCommand(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            UserRepository = userRepository;
            TokenRepository = tokenRepository;
        }

        public TokenOutput Execute(TokenInput input)
        {
            var user = UserRepository.FindUserWithLogin(input.Login);

            if (user.IsNull() || input.Password != user.Password)
            {
                throw UserException.NotFound(user.Login);
            }

            var token = new MyToken(user, "JopaToken", DateTime.Now);
            TokenRepository.Create(token);

            return new TokenOutput(token);
        }
    }
}