using System;
using Domain.Entities;

namespace Application.CQS.User.Output
{
    public class ProfileOutput
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }
        public UserRole Role { get; }
        public string? Avatar { get; }

        public ProfileOutput(UserEntity user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            Role = user.Role;
            Avatar = user.Avatar;
        }
    }
}
