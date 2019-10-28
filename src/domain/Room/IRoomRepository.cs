using System.Linq;

namespace Domain.Room
{
    public interface IRoomRepository : IEntityRepository<RoomEntity>
    {
        IQueryable<RoomEntity> FindAllRooms();
    }
}