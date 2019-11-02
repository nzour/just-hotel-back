using System.Collections.Generic;
using System.Linq;
using Domain.Rent;

#nullable disable

namespace Domain.User
{
    public class UserEntity : AbstractEntity
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
        public string Role { get; internal set; }
        public ISet<RentEntity> Rents { get; internal set; } = new HashSet<RentEntity>();

        // Nhibernate требует наличие безаргументного конструктора.
        protected UserEntity()
        {
        }

        public UserEntity(string firstName, string lastName, string login, string password, string role)
        {
            UserRole.Validate(role);

            Identify();
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public static class UserRole
    {
        public const string Manager = "Manager";
        public const string Employee = "Employee";
        public const string Client = "Client";

        public static IEnumerable<string> Roles { get; } = new[] { Manager, Employee, Client };

        public static void Validate(string role)
        {
            if (Roles.Contains(role))
            {
                return;
            }

            throw UserException.RoleIsNotValid(role, Roles);
        }
    }
}