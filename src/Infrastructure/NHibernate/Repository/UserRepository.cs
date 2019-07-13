using app.Domain.Entity.User;

namespace app.Infrastructure.NHibernate.Repository
{
    public class UserRepository : AbstractSessionAware, IUserRepository
    {
    }
}