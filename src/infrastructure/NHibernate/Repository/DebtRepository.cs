using Domain.Debt;
using Domain.DebtEntity;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class DebtRepository : EntityRepository<DebtEntity>, IDebtRepository
    {
        public DebtRepository(ISession session) : base(session)
        {
        }
    }
}