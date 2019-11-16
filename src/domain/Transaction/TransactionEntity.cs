using System;
using System.Collections.Generic;
using Domain.Rent;
using Domain.Room;
using Domain.Service;
using Domain.User;

namespace Domain.Transaction
{
    public class TransactionEntity : AbstractEntity
    {
        public UserEntity User { get; }
        public RoomEntity Room { get; }
        public IEnumerable<ServiceEntity> Services { get; }
        public uint Money { get; }
        public DateTime CreatedAt { get; }

        public TransactionEntity(RentEntity rent)
        {
            Identify();

            User = rent.User;
            Room = rent.Room;
            Services = rent.Services;
            Money = rent.Cost;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
