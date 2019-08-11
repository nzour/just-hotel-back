using System;
using NHibernate;

namespace app.Infrastructure.NHibernate
{
    /// <summary>
    /// Умеет оборачивать делегат в транзакцию
    /// </summary>
    public static class Transactional
    {
        private static ISession Session { get; }
        private static ITransaction Transaction { get; }
        
        static Transactional()
        {
            Session = NHibernateHelper.OpenSession();
            Transaction = Session.Transaction;
        }

        /// <summary>
        /// Обернет делегат в транзакцию и вернет результат выполнения делегата.
        /// </summary>
        /// <param name="function">Делегат, который произойдет внутри транзакции</param>
        /// <typeparam name="T">Тип, который вернет делегат</typeparam>
        public static TResult Action<TResult>(Func<TResult> function)
        {
            try
            {
                Begin();

                var result = function.Invoke();

                Commit();

                return result;
            }
            catch
            {
                RollBack();
                throw;
            }
            finally
            {
                
                Transaction.Dispose();
            }
        }

        /// <summary>
        /// Обернет VoidResult-делегат в транзакцию.
        /// </summary>
        /// <param name="function"></param>
        public static void Action(Action function)
        {
            try
            {
                Begin();
                
                function.Invoke();
                
                Commit();
            }
            catch
            {
                RollBack();
                throw;
            }
            finally
            {
                Transaction.Dispose();
            }
        }

        private static void Begin()
        {
            if (!Transaction.IsActive)
            {
                Transaction.Begin();
            }
        }

        private static void Commit()
        {
            Session.Flush();
            
            if (!Transaction.WasCommitted)
            {
                Transaction.Commit();
            }
        }

        private static void RollBack()
        {
            if (!Transaction.WasRolledBack)
            {
                Transaction.Rollback();
            }
        }
    }
}