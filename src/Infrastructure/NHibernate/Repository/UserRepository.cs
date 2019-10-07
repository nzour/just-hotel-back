using System;
using app.Domain.User;
using common.Extensions;

namespace app.Infrastructure.NHibernate.Repository
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(Transactional transactional) : base(transactional)
        {
        }

        public void Create(User user)
        {
            Transactional.Action(session => session.Save(user));
        }

        public User GetUser(Guid id)
        {
            return Transactional.WithSession(session =>
            {
                var user = session.Get<User>(id);
                return user.AssertNull(() => throw UserNotFoundException.WithId(id));
            });
        }

        public User FindUserWithLogin(string login)
        {
            return Transactional.WithSession(session =>
            {
                return session.QueryOver<User>()
                    .Where(u => u.Login == login)
                    .SingleOrDefault();
            });
        }
    }
}