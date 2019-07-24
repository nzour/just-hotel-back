namespace app.Domain.Entity.User
{
    public class User : AbstractEntity
    {
        public virtual string Name { get; protected internal set; }
        public virtual string Login { get; protected internal set; }
        public virtual string Password { get; protected internal set; }

        // Nhibernate требует наличие безаргументного конструктора.
        protected User()
        {
        }

        public User(string name, string login, string password)
        {
            Identify();

            Name = name;
            Login = login;
            Password = password;
        }
    }
}