using System;
using Domain.User;

namespace Application.CQS.User.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public UserOutput(UserEntity user)
        {
            Id = user.Id;
            Role = user.Role.ToString();
            Login = user.Login;
            Firstname = user.FirstName;
            Lastname = user.LastName;
        }
    }
}