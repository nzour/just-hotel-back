using System.Linq;
using Common.Util;
using Domain.Room;

namespace Application.CQS.Room.Input
{
    public class GetRoomInputFilter : IInputFilter<RoomEntity>
    {
        public bool? IsRented { get; set; }
        public string? RoomType { get; set; }

        public IQueryable<RoomEntity> Process(IQueryable<RoomEntity> query)
        {
            if (null != IsRented)
            {
                query = query.Where(room => IsRented == room.IsBusy);
            }

            if (null != RoomType)
            {
                query = query.Where(room => RoomType == room.RoomType);
            }

            return query;
        }
    }
}