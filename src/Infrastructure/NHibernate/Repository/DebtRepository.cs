using App.Domain.DebtEntity;
using NHibernate;

namespace App.Infrastructure.NHibernate.Repository
{
    public class DebtRepository : EntityRepository<Debt>, IDebtRepository
    {
        public DebtRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional, sessionFactory)
        {
        }
    }
}