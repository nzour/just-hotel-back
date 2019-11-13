using System;
using Domain.User;

namespace Application.CQS.Auth.Output
{
    public class SignInOutput
    {
        public Guid UserId { get; }
        public string Token { get; }
        public string Role { get; }

        public SignInOutput(UserEntity user, string token)
        {
            Token = token;
            UserId = user.Id;
            Role = user.Role.ToString();
        }
    }
}