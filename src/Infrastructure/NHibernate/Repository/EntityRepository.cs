using System;
using App.Domain;
using NHibernate;

namespace App.Infrastructure.NHibernate.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : AbstractEntity
    {
        public Transactional Transactional { get; }
        public ISessionFactory SessionFactory { get; }
        public EntityRepository(Transactional transactional, ISessionFactory sessionFactory)
        {
            Transactional = transactional;
            SessionFactory = sessionFactory;
        }

        public void Save(T entity)
        {
            var session = SessionFactory.OpenSession();

            session.SaveOrUpdate(entity);

            session.Flush();
        }

        public T Get(Guid id)
        {
            var entity = Transactional.Func(session => session.Get<T>(id));

            if (entity == null)
            {
                throw new EntityNotFoundException<T>(id);
            }

            return entity;
        }

        public T Get(string id) => Get(new Guid(id));

        public void Save(params T[] entities)
        {
            Transactional.Action(session =>
            {
                foreach (var entity in entities)
                {
                    session.SaveOrUpdate(entity);
                }
            });
        }

        public void SaveAsync(params T[] entities)
        {
            Transactional.Action(session =>
            {
                foreach (var entity in entities)
                {
                    session.SaveOrUpdateAsync(entity);
                }
            });
        }
    }
}