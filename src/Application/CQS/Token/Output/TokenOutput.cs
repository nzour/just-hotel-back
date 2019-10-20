using System;

namespace app.Application.CQS.Token.Output
{
    public class TokenOutput
    {
        public Guid UserId { get; }
        public string Token { get; }

        public TokenOutput(Guid userId, string token)
        {
            Token = token;
            UserId = userId;
        }
    }
}