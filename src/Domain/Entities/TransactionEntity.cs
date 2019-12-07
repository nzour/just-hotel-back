using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class TransactionEntity
    {
        public Guid Id { get; }
        public UserEntity User { get; }
        public RoomEntity Room { get; }
        public IEnumerable<ServiceEntity> Services { get; }
        public int Money { get; }
        public DateTime CreatedAt { get; }

        public TransactionEntity(UserEntity user, RoomEntity room, IEnumerable<ServiceEntity> services)
        {
            Id = Guid.NewGuid();
            User = user;
            Room = room;
            Services = services;
            Money = room.Cost + services.Sum(s => s.Cost);
            CreatedAt = DateTime.UtcNow;
        }
    }
}
