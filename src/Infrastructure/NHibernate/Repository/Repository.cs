using System.Linq;
using System.Threading.Tasks;
using Domain;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ISession Session { get; }

        public Repository(ISession session)
        {
            Session = session;
        }

        public void SaveAndFlush(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                Session.Save(entity);
            }

            Session.Flush();
        }

        public async Task SaveAndFlushAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                await Session.SaveAsync(entity);
            }

            await Session.FlushAsync();
        }

        public TEntity Get(object id)
        {
            var entity = Session.Get<TEntity>(id);

            if (null == entity)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(object id)
        {
            return await Session.GetAsync<TEntity>(id);
        }

        public void Delete(object id)
        {
            Session.Delete(id);
            Session.Flush();
        }

        public async Task DeleteAsync(object id)
        {
            await Session.DeleteAsync(id);
            await Session.FlushAsync();
        }

        public IQueryable<TEntity> FindAll()
        {
            return Session.Query<TEntity>();
        }
    }
}
