#nullable disable

namespace App.Domain.UserEntity
{
    public class User : AbstractEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }

        // Nhibernate требует наличие безаргументного конструктора.
        protected User()
        {
        }

        public User(string firstName, string lastName, string login, string password)
        {
            Identify();

            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
        }
    }
}