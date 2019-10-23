#nullable disable

namespace App.Domain.UserEntity
{
    public class User : AbstractEntity
    {
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Login { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual UserRole Role { get; protected set; }

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