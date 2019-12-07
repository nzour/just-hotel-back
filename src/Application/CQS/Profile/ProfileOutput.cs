using System;
using Domain.Entities;

namespace Application.CQS.Profile
{
    public class ProfileOutput
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }
        public UserRole Role { get; }

        public ProfileOutput(UserEntity entity)
        {
            Id = entity.Id;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Login = entity.Login;
            Role = entity.Role;
        }
    }
}
