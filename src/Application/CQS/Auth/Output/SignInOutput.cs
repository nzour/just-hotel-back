using System;

namespace app.Application.CQS.Auth.Output
{
    public class SignInOutput
    {
        public Guid UserId { get; }
        public string Token { get; }

        public SignInOutput(Guid userId, string token)
        {
            Token = token;
            UserId = userId;
        }
    }
}