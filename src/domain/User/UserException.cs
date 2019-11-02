using System;
using System.Collections.Generic;

namespace Domain.User
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }

        public static UserException RoleIsNotValid(string invalid, IEnumerable<string> valid)
        {
            return new UserException($"Role '{invalid}' is invalid. Valid roles: {string.Join(", ", valid)}");
        }
    }
}