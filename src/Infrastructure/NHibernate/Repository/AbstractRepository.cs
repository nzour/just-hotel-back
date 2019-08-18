using NHibernate;

namespace app.Infrastructure.NHibernate.Repository
{
    public abstract class AbstractRepository
    {
        protected Transactional Transactional { get; }

        protected AbstractRepository(Transactional transactional)
        {
            Transactional = transactional;
        }
    }
}