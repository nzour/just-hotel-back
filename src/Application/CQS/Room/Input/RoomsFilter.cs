using System.Collections.Generic;
using System.Linq;
using Common.Util;
using Domain.Entities;
using FluentNHibernate.Conventions;

namespace Application.CQS.Room.Input
{
    public class RoomsFilter : IFilter<RoomEntity>
    {
        public IEnumerable<RoomType>? RoomTypes { get; set; }

        public IQueryable<RoomEntity> Process(IQueryable<RoomEntity> query)
        {
            if (null != RoomTypes && RoomTypes.IsNotEmpty())
            {
                query = query.Where(r => RoomTypes.Contains(r.RoomType));
            }

            return query;
        }
    }
}