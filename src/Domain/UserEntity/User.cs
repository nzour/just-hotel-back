#nullable disable

namespace App.Domain.UserEntity
{
    public class User : AbstractEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public UserRole Role { get; private set; }

        // Nhibernate требует наличие безаргументного конструктора.
        protected User()
        {
        }
        
        public User(string firstName, string lastName, string login, string password, UserRole role)
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