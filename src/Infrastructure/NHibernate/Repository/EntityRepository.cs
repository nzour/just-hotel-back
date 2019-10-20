using System;
using app.Domain;

namespace app.Infrastructure.NHibernate.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        public Transactional Transactional { get; }

        public EntityRepository(Transactional transactional)
        {
            Transactional = transactional;
        }

        public void Save(T entity)
        {
            Transactional.Action(session => session.Save(entity));
        }

        public T Get(Guid id)
        {
            return Transactional.Func(session => session.Get<T>(id));
        }

        public T Get(string id) => Get(new Guid(id));

        public void Save(params T[] entities)
        {
            Transactional.Action(session =>
            {
                foreach (var entity in entities)
                {
                    session.Save(entity);
                }
            });
        }
    }
}