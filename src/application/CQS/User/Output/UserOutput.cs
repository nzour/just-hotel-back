using System;
using Domain.User;

#nullable disable

namespace Application.CQS.User.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public static implicit operator UserOutput(UserEntity userEntity)
        {
            return new UserOutput
            {
                Id = userEntity.Id,
                Role = userEntity.Role,
                Login = userEntity.Login,
                Firstname = userEntity.FirstName,
                Lastname = userEntity.LastName
            };
        }
    }
}