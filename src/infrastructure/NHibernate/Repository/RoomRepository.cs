using System.Linq;
using Domain.Room;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class RoomRepository : EntityRepository<RoomEntity>, IRoomRepository
    {
        public RoomRepository(ISession session) : base(session)
        {
        }
    }
}