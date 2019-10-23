using System;
using System.Threading.Tasks;

namespace App.Domain
{
    public interface IEntityRepository<T> where T : class
    {
        void Save(T entity);
        T Get(Guid id);
        T Get(string id);

        void Save(params T[] entities);
        void SaveAsync(params T[] entities);
    }
}