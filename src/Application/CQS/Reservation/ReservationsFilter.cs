using System;
using System.Linq;
using Common.Util;
using Domain.Entities;

namespace Application.CQS.Reservation
{
    public class ReservationsFilter : IFilter<ReservationEntity>
    {
        public Guid? UserId { get; set; }
        public Guid? RoomId { get; set; }

        public IQueryable<ReservationEntity> Process(IQueryable<ReservationEntity> query)
        {
            if (null != UserId)
            {
                query = query.Where(r => UserId == r.User.Id);
            }

            if (null != RoomId)
            {
                query = query.Where(r => RoomId == r.Room.Id);
            }


            return query;
        }
    }
}
