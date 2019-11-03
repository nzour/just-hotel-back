using System.Collections.Generic;
using System.Linq;
using Common.Util;
using Domain.Room;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;

namespace Application.CQS.Room.Input
{
    public class GetRoomInputFilter : IInputFilter<RoomEntity>
    {
        public bool? IsRented { get; set; }
        public IEnumerable<string>? RoomTypes { get; set; }

        public IQueryable<RoomEntity> Process(IQueryable<RoomEntity> query)
        {
            if (null != IsRented)
            {
                query = query.Where(room => IsRented == room.IsBusy);
            }

            if (null != RoomTypes && RoomTypes.IsNotEmpty())
            {
                query = query.Where(room => room.RoomType.In(RoomTypes.ToArray()));
            }

            return query;
        }
    }
}