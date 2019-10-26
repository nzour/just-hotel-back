using Domain.DebtEntity;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class DebtRepository : EntityRepository<Debt>, IDebtRepository
    {
        public DebtRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional,
            sessionFactory)
        {
        }
    }
}