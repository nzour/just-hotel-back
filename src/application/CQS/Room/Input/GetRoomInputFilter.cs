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
        public IEnumerable<RoomType>? RoomTypes { get; set; }

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
                query = query.Where(r => r.RoomType.In(RoomTypes.ToArray()));
            }

            return query;
        }
    }
}