using NHibernate;

namespace app.Infrastructure.NHibernate.Repository
{
    public abstract class AbstractRepository
    {
        protected ISession Session { get; } = NHibernateHelper.OpenSession();
    }
}