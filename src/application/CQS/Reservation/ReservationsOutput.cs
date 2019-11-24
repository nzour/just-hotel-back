using System;
using System.Collections.Generic;
using System.Linq;
using Application.CQS.Room.Output;
using Application.CQS.Service.Output;
using Application.CQS.User.Output;
using Domain.Reservation;

namespace Application.CQS.Reservation
{
    public class ReservationsOutput
    {
        public Guid Id { get; }
        public UserOutput User { get; }
        public RoomOutput Room { get; }
        public DateTime ReservedFrom { get; }
        public DateTime ReservedTo { get; }
        public uint Cost { get; }
        public IEnumerable<ServiceOutput> Services { get; }

        public ReservationsOutput(ReservationEntity reservation)
        {
            Id = reservation.Id;
            User = new UserOutput(reservation.User);
            Room = new RoomOutput(reservation.Room);
            ReservedFrom = reservation.ReservedFrom;
            ReservedTo = reservation.ReservedTo;
            Cost = reservation.Cost;
            Services = reservation.Services.Select(s => new ServiceOutput(s));
        }
    }
}
