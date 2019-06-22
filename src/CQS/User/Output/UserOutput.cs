using System;

namespace app.CQS.User.Output
{
    public class UserOutput
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Login { get; }

        public UserOutput(Modules.ORM.Entities.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.Login;
        }
    }
}