using System.Linq;
using System.Threading.Tasks;
using Domain.User;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class UserRepository : EntityRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public async Task CreateUserAsync(UserEntity user)
        {
            await Task.Run(() => Save(user));
        }

        public void CreateUser(UserEntity userEntity)
        {
            Save(userEntity);
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