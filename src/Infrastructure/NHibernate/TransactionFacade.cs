using System;
using System.Threading.Tasks;
using NHibernate;

namespace Infrastructure.NHibernate
{
    /// <summary>
    /// Фасад для транзакций из NHibernate
    /// </summary>
    public sealed class TransactionFacade : IDisposable
    {
        private ISession Session { get; }
        public bool IsActive => Session.Transaction.IsActive;

        public TransactionFacade(ISession session)
        {
            Session = session;
        }

        public async Task ActionAsync(Action action)
        {
            Begin();

            try
            {
                action();

                await Session.FlushAsync();
                await Session.Transaction.CommitAsync();
            }
            catch
            {
                await Session.Transaction.RollbackAsync();
                throw;
            }

        }
        
        public void Begin()
        {
            if (Session.Transaction.IsActive)
            {
                return;
            }

            Session.Transaction.Begin();
        }

        public void Commit()
        {
            Session.Flush();

            if (Session.Transaction.WasCommitted)
            {
                return;
            }

            Session.Transaction.Commit();
        }

        public void Rollback()
        {
            if (Session.Transaction.WasRolledBack)
            {
                return;
            }

            Session.Transaction.Rollback();
        }

        public void Dispose()
        {
            if (!Session.Transaction.IsActive)
            {
                return;
            }

            Session.Transaction.Dispose();
        }
    }

    public class NotTransactionalAttribute : Attribute
    {
    }
}