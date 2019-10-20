using System;
using System.Threading.Tasks;
using app.Domain.User;
using common.Extensions;

namespace app.Infrastructure.NHibernate.Repository
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(Transactional transactional) : base(transactional)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await Task.Run(() => Transactional.Action(session => session.Save(user))) ;
        }

        public User GetUser(Guid id)
        {
            return Transactional.WithSession(session =>
            {
                var user = session.Get<User>(id);
                return user.AssertNull(() => throw UserNotFoundException.WithId(id));
            });
        }

        public User FindUserWithLoginAndPassword(string login, string encryptedPassword)
        {
            return Transactional.WithSession(session =>
            {
                return session.QueryOver<User>()
                    .Where(u => login == u.Login && encryptedPassword == u.Password)
                    .SingleOrDefault();
            });
        }
    }
}