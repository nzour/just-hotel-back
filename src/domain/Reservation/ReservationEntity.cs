using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Room;
using Domain.Service;
using Domain.User;

namespace Domain.Reservation
{
    public class ReservationEntity : AbstractEntity
    {
        public UserEntity User { get; }
        public RoomEntity Room { get; }
        public DateTime ReservedFrom { get; }
        public DateTime ReservedTo { get; }
        public IEnumerable<ServiceEntity> Services { get; }
        public uint Cost => Room.Cost + (uint) Services.Sum(s => s.Cost);

        public ReservationEntity(UserEntity user, RoomEntity room, DateTime from, DateTime to, IEnumerable<ServiceEntity> services)
        {
            ReservationException.AssertDatesValid(from, to);
            
            Identify();
            User = user;
            Room = room;
            ReservedFrom = from;
            ReservedTo = to;
            Services = services;
        }
    }
}
