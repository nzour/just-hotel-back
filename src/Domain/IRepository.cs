using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task SaveAndFlushAsync(params TEntity[] entities);

        Task<TEntity> GetAsync(object id);

        Task DeleteAndFlushAsync(object id);

        IQueryable<TEntity> FindAll();
    }
}