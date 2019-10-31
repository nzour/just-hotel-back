using System;
using NHibernate;

namespace Infrastructure.NHibernate
{
    /// <summary>
    ///     Умеет оборачивать делегат в транзакцию
    /// </summary>
    public sealed class Transactional
    {
        private ISession Session { get; }

        public Transactional(ISession session)
        {
            Session = session;
        }

        public TResult Action<TResult>(Func<ISession, TResult> function)
        {
            BeginTransaction();

            try
            {
                var result = function.Invoke(Session);

                CommitTransaction();

                return result;
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public void Action(Action<ISession> function)
        {
            BeginTransaction();

            try
            {
                function.Invoke(Session);
                CommitTransaction();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public void Action(Action function)
        {
            BeginTransaction();

            try
            {
                function.Invoke();
                CommitTransaction();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        private void BeginTransaction()
        {
            if (Session.Transaction.IsActive)
            {
                return;
            }

            Session.Transaction.Begin();
        }

        private void CommitTransaction()
        {
            Session.Flush();

            if (Session.Transaction.WasCommitted)
            {
                return;
            }

            Session.Transaction.Commit();
        }

        private void RollBackTransaction()
        {
            if (Session.Transaction.WasRolledBack)
            {
                return;
            }

            Session.Transaction.Rollback();
        }

        private void DisposeTransaction()
        {
            if (!Session.Transaction.IsActive)
            {
                return;
            }

            Session.Transaction.Dispose();
        }
    }
}