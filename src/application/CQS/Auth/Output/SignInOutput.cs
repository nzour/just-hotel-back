using System;

namespace Application.CQS.Auth.Output
{
    public class SignInOutput
    {
        public SignInOutput(Guid userId, string token)
        {
            Token = token;
            UserId = userId;
        }

        public Guid UserId { get; }
        public string Token { get; }
    }
}