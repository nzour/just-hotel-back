using Domain.Debt;
using Domain.DebtEntity;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class DebtRepository : EntityRepository<DebtEntity>, IDebtRepository
    {
        public DebtRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional,
            sessionFactory)
        {
        }
    }
}