using System;
using NHibernate;

namespace app.Infrastructure.NHibernate
{
    public static class Transactional
    {
        private static ITransaction Transaction { get; set; }
        
        static Transactional()
        {
            Transaction = NHibernateHelper.OpenSession().Transaction;
        }

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
                Transaction.Rollback();
                throw;
            }

        }
    }
}