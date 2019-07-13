using System;
using NHibernate;

namespace app.Infrastructure.NHibernate
{
    /// <summary>
    /// Умеет оборачивать делегат в транзакцию
    /// </summary>
    public static class Transactional
    {
        private static ITransaction Transaction { get; }
        
        static Transactional()
        {
            Transaction = NHibernateHelper.OpenSession().Transaction;
        }

        /// <summary>
        /// Обернет делегат в транзакцию и вернет результат выполнения делегата.
        /// </summary>
        /// <param name="function">Делегат, который произойдет внутри транзакции</param>
        /// <typeparam name="T">Тип, который вернет делегат</typeparam>
        public static T Action<T>(Func<T> function)
        {
            try
            {
                if (!Transaction.IsActive)
                {
                    Transaction.Begin();
                }

                var result =  function.Invoke();

                if (!Transaction.WasCommitted)
                {
                    Transaction.Commit();
                }

                return result;
            }
            catch (Exception)
            {
                if (!Transaction.WasRolledBack)
                {
                    Transaction.Rollback();
                }
                
                throw;
            }

        }
    }
}