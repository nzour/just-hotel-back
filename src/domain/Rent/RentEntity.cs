using System;
using Domain.Room;
using Domain.User;

#nullable disable

namespace Domain.Rent
{
    public class RentEntity : AbstractEntity
    {
        public RoomEntity Room { get; internal set; }
        public UserEntity User { get; internal set; }
        public DateTime StartedAt { get; internal set; }
        public DateTime ExpiredAt { get; internal set; }

        protected RentEntity()
        {
        }

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
        }
    }
}