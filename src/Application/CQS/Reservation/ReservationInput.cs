using System;
using System.Collections.Generic;

namespace Application.CQS.Reservation
{
    public class ReservationInput
    {
        public Guid RoomId { get; set; }
        public DateTime ReservedFrom { get; set; }
        public DateTime ReservedTo { get; set; }
        public IEnumerable<Guid> ServiceIds { get; set; }

        public ReservationInput(Guid roomId, DateTime reservedFrom, DateTime reservedTo, IEnumerable<Guid> serviceIds)
        {
            RoomId = roomId;
            ReservedFrom = reservedFrom;
            ReservedTo = reservedTo;
            ServiceIds = serviceIds;
        }
    }
}
