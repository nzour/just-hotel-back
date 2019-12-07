using System;
using Domain.Entities;

namespace Application.CQS.Auth.Output
{
    public class SignInOutput
    {
        public Guid UserId { get; }
        public string Token { get; }
        public string Role { get; }

        public SignInOutput(UserEntity userEntity, string token)
        {
            Token = token;
            UserId = userEntity.Id;
            Role = userEntity.Role.ToString();
        }
    }
}
