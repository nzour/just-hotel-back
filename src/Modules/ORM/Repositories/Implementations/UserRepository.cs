using System;
using System.Collections.Generic;
using app.Modules.ORM.Entities;
using app.Modules.ORM.Repositories.Interfaces;
using NHibernate;

namespace app.Modules.ORM.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private ISession _session;
        private ITransaction _transaction;

        public UserRepository(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }
        
        public void AddUser(User user)
        {
            _session.Save(user);
            _transaction.Commit();
        }

        public User FindUser(Guid id)
        {
            return _session.Get<User>(id);
        }

        public List<User> FindAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}