using System.Collections.Generic;
using System.Linq;

namespace app.Domain.User
{
    public class User : AbstractEntity
    {
        public const string AdminRole = "ROLE_ADMIN";
        public const string DefaultRole = "ROLE_DEFAULT";

        public virtual string Name { get; protected internal set; }
        public virtual string Login { get; protected internal set; }
        public virtual string Password { get; protected internal set; }
        public virtual IEnumerable<string> Roles { get; set; }

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
            Roles = new[] { DefaultRole };
        }

        public virtual void UpdateName(string name)
        {
            Name = name;
        }

        public virtual void AssignRoles(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (Roles.Contains(role))
                {
                    continue;
                }

                Roles.Append(role);
            }
        }
    }
}