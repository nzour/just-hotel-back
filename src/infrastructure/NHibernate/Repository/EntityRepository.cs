using System;
using System.Linq;
using Domain;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : AbstractEntity
    {
        protected ISession Session { get; }

        public EntityRepository(ISession session)
        {
            Session = session;
        }

        public void Save(TEntity entity)
        {
            Session.Save(entity);
        }

        public void Save(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                Save(entity);
            }
        }

        public TEntity Get(Guid id)
        {
            var entity = Session.Get<TEntity>(id);

            if (null == entity)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            return entity;
        }

        public TEntity Get(string id)
        {
            return Get(new Guid(id));
        }

        public IQueryable<TEntity> FindAll()
        {
            return Session.Query<TEntity>();
        }
    }
}