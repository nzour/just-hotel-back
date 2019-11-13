using System.Collections.Generic;
using Domain.Rent;

namespace Domain.User
{
    public class UserEntity : AbstractEntity
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public UserRole Role { get; internal set; }
        public ISet<RentEntity> Rents { get; internal set; } = new HashSet<RentEntity>();

        public UserEntity(string firstName, string lastName, string login, string password, UserRole role)
        {
            Identify();
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public enum UserRole
    {
        Manager,
        Employee,
        Client
    }
}