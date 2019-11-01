using System;
using System.Linq;

namespace Domain
{
    public interface IEntityRepository<T> where T : class
    {
        void SaveAndFlush(T entity);
        void SaveAndFlush(params T[] entities);

        T Get(Guid id);
        T Get(string id);

        IQueryable<T> FindAll();
    }
}