using System;

namespace app.Domain.Entity.User
{
    public interface IUserRepository
    {
        void Create(User user);
        
        User GetUser(Guid id);

        /// <returns>User or null</returns>
        User FindUserWithLogin(string login);
    }
}