using System.Linq;
using System.Threading.Tasks;
using App.Domain.UserEntity;

namespace App.Infrastructure.NHibernate.Repository
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(Transactional transactional) : base(transactional)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await Task.Run(() => Transactional.Action(session => session.Save(user)));
        }

        public User FindUserWithLoginAndPassword(string login, string encryptedPassword)
        {
            return Transactional.Func(session =>
            {
                return session.Query<User>()
                    .SingleOrDefault(u => login == u.Login && encryptedPassword == u.Password);
            });
        }

        
        public bool IsLoginBusy(string login)
        {
            return Transactional.Func(session =>
                null != session.Query<User>().SingleOrDefault(u => u.Login == login)
            );
        }
    }
}