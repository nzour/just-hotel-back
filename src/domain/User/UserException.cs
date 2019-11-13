using System;

namespace Domain.User
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }
}