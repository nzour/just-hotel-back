using System;
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

        public TResult Action<TResult>(Func<ISession, TResult> function)
        {
            Begin();

            try
            {
                var result = function.Invoke(Session);

                Commit();

                return result;
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Action(Action<ISession> function)
        {
            Begin();

            try
            {
                function.Invoke(Session);
                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Action(Action function)
        {
            Begin();

            try
            {
                function.Invoke();
                Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
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
}