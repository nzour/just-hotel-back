using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Room;
using Domain.Service;
using Domain.User;

namespace Domain.Rent
{
    public class RentEntity : AbstractEntity
    {
        public RoomEntity Room { get; protected internal set; }
        public UserEntity User { get; protected internal set; }
        public DateTime StartedAt { get; protected internal set; }
        public DateTime ExpiredAt { get; protected internal set; }
        public ISet<ServiceEntity> Services { get; protected internal set; }
        public int Cost => Services.Sum(s => s.Cost) + Room.Cost;

        public RentEntity(RoomEntity room, UserEntity user, DateTime startedAt, DateTime expiredAt)
        {
            if (expiredAt < startedAt)
            {
                throw RentException.InvalidDates();
            }

            Id = room.Id;
            Room = room;
            User = user;
            StartedAt = startedAt;
            ExpiredAt = expiredAt;
            Services = new HashSet<ServiceEntity>();
        }
    }
}