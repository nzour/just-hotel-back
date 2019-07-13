namespace app.Domain.Entity.User
{
    public class User : AbstractEntity
    {
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        public User(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}