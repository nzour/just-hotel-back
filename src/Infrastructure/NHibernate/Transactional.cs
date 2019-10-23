using System;
using NHibernate;

namespace App.Infrastructure.NHibernate
{
    /// <summary>
    /// Умеет оборачивать делегат в транзакцию
    /// </summary>
    public class Transactional
    {
        public ISessionFactory SessionFactory { get; }

        public Transactional(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        /// <summary>
        /// Обернет делегат в транзакцию и вернет результат выполнения делегата.
        /// </summary>
        /// <param name="function">Делегат, который произойдет внутри транзакции</param>
        /// <typeparam name="T">Тип, который вернет делегат</typeparam>
        public TResult Action<TResult>(Func<ISession, TResult> function)
        {
            var session = Begin();

            try
            {
                var result = function.Invoke(session);

                Commit(session);

                return result;
            }
            catch
            {
                RollBack(session);
                throw;
            }
            finally
            {
                session.Transaction.Dispose();
                session.Dispose();
            }
        }

        /// <summary>
        /// Обернет VoidResult-делегат в транзакцию.
        /// </summary>
        /// <param name="function"></param>
        public void Action(Action<ISession> function)
        {
            var session = Begin();

            try
            {
                function.Invoke(session);

                Commit(session);
            }
            catch
            {
                RollBack(session);
                throw;
            }
            finally
            {
                session.Transaction.Dispose();
                session.Dispose();
            }
        }

        public void Func(Action<ISession> action)
        {
            var session = SessionFactory.OpenSession();

            try
            {
                action.Invoke(session);
            }
            finally
            {
                session.Dispose();
            }
        }

        public TResult Func<TResult>(Func<ISession, TResult> action)
        {
            var session = SessionFactory.OpenSession();

            try
            {
                return action.Invoke(session);
            }
            finally
            {
                session.Dispose();
            }
        }

        private ISession Begin()
        {
            var session = SessionFactory.OpenSession();

            if (!session.Transaction.IsActive)
            {
                session.Transaction.Begin();
            }

            return session;
        }

        private void Commit(ISession session)
        {
            session.Flush();

            if (!session.Transaction.WasCommitted)
            {
                session.Transaction.Commit();
            }
        }

        private void RollBack(ISession session)
        {
            if (!session.Transaction.WasRolledBack)
            {
                session.Transaction.Rollback();
            }
        }
    }
}