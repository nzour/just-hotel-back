using System;
using System.Collections.Generic;
using app.Modules.ORM.Entities;

namespace app.Modules.ORM.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User FindUser(Guid id);
        List<User> FindAllUsers();
    }
}