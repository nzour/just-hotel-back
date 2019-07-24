using System;

namespace app.Application.CQS.User.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }

        public UserOutput(Domain.Entity.User.User user)
        {
            Id = user.Id;
            Name = user.Name;
            Login = user.Login;
        }
    }
}