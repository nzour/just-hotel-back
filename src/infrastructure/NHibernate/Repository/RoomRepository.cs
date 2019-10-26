using Domain.RoomEntity;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class RoomRepository : EntityRepository<Room>, IRoomRepository
    {
        public RoomRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional,
            sessionFactory)
        {
        }
    }
}