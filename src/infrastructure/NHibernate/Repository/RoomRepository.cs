using System.Linq;
using Domain.Room;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class RoomRepository : EntityRepository<RoomEntity>, IRoomRepository
    {
        public RoomRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional,
            sessionFactory)
        {
        }

        public IQueryable<RoomEntity> FindAllRooms()
        {
            using var session = SessionFactory.OpenSession();
            return session.Query<RoomEntity>();
        }
    }
}