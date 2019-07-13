using app.Domain.Entity.User;
using Npgsql;

namespace app.Infrastructure.NHibernate.Repository
{
    public class UserRepository : AbstractSessionAware, IUserRepository
    {
        public User GetUser()
        {
            return Transactional.Action(() =>
                new User("Zobor", "Zobor-Login", "Password"));
        }
    }
}