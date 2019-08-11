using System;
using app.Common.Extensions;
using app.Domain.Entity.User;

namespace app.Infrastructure.NHibernate.Repository
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public void Create(User user)
        {
            Transactional.Action(() => Session.Persist(user));
        }
        
        public User GetUser(Guid id)
        {
            var user = Session.Get<User>(id);

            return user.AssertNull(() => throw UserException.NotFound(id));
        }

        public User FindUserWithLogin(string login)
        {
            return Session.QueryOver<User>()
                .Where(u => u.Login == login)
                .SingleOrDefault();
        }
    }
}