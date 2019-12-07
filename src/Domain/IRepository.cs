using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void SaveAndFlush(params TEntity[] entities);
        Task SaveAndFlushAsync(params TEntity[] entities);

        TEntity Get(object id);
        Task<TEntity> GetAsync(object id);

        void DeleteAndFlush(object id);
        Task DeleteAndFlushAsync(object id);

        IQueryable<TEntity> FindAll();
    }
}