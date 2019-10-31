using System;
using System.Linq;

namespace Domain
{
    public interface IEntityRepository<T> where T : AbstractEntity
    {
        void Save(T entity);
        void Save(params T[] entities);

        T Get(Guid id);
        T Get(string id);

        IQueryable<T> FindAll();
    }
}