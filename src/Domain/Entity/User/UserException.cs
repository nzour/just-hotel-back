using System;

namespace app.Domain.Entity.User
{
    public class UserException : AbstractException
    {
        public UserException(string message) : base(message)
        {
        }

        public static UserException NotFound(Guid id)
        {
            return new UserException($"User with id {id} was not found.");
        }

        public static UserException NotFound(string login)
        {
            return new UserException($"User with login {login} was not found.");
        }
    }
}