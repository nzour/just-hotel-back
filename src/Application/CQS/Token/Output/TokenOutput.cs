using System;
using _Token = app.Domain.Entity.Token.Token;

namespace app.Application.CQS.Token.Output
{
    public class TokenOutput
    {
        public Guid UserId { get; }
        public string AccessToken { get; }

        public TokenOutput(_Token token)
        {
            UserId = token.User.Id;
            AccessToken = token.AccessToken;
        }
    }
}