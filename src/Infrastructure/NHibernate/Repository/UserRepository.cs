using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure.NHibernate.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
        }

        public async Task<UserEntity?> FindUserAsync(string login, string encryptedPassword)
        {
            return await Session
                .Query<UserEntity>()
                .SingleOrDefaultAsync(u => login == u.Login && encryptedPassword == u.Password);
        }

        public async Task<bool> IsLoginBusyAsync(string login)
        {
            return 0 != await Session
                       .Query<UserEntity>()
                       .CountAsync(u => u.Login == login);
        }
    }
}
