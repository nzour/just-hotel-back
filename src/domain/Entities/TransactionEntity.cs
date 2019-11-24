using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class TransactionEntity : AbstractEntity
    {
        public UserEntity User { get; }
        public RoomEntity Room { get; }
        public IEnumerable<ServiceEntity> Services { get; }
        public uint Money { get; }
        public DateTime CreatedAt { get; }

        public TransactionEntity(UserEntity user, RoomEntity room, IEnumerable<ServiceEntity> services)
        {
            Identify();

            User = user;
            Room = room;
            Services = services;
            Money = room.Cost + (uint) services.Sum(s => s.Cost);
            CreatedAt = DateTime.UtcNow;
        }
    }
}
