using System.Collections.Generic;
using System.Linq;
using Common.Util;
using Domain.Room;
using FluentNHibernate.Conventions;

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
                query = true == IsRented
                    ? query.Where(r => null != r.Rent)
                    : query.Where(r => null == r.Rent);
            }

            if (null != RoomTypes && RoomTypes.IsNotEmpty())
            {
                query = query.Where(r => RoomTypes.Contains(r.RoomType));
            }

            return query;
        }
    }
}