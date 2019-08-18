using Newtonsoft.Json;

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

        public virtual void ChangeName(string name)
        {
            Name = name;
        }
        
        public static implicit operator string(User user)
        {
            return JsonConvert.SerializeObject(new UserPayload(user));
        }
    }

    internal class UserPayload
    {
        public string Id { get; }
        public string Name { get; }
        public string Login { get; }
        
        public UserPayload(User user)
        {
            Id = user.Id.ToString();
            Name = user.Name;
            Login = user.Login;
        }
    }
}