using System.Linq;
using Domain.User;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class UserRepository : EntityRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public UserEntity FindUserWithLoginAndPassword(string login, string encryptedPassword)
        {
            return Session.Query<UserEntity>()
                .SingleOrDefault(u => login == u.Login && encryptedPassword == u.Password);
        }

        public bool IsLoginBusy(string login)
        {
            return null != Session.Query<UserEntity>()
                       .SingleOrDefault(u => u.Login == login);
        }
    }
}