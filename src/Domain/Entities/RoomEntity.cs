using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public enum RoomType
    {
        Single,
        Double,
        Triple
    }

    public class RoomEntity
    {
        public Guid Id { get; }
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }
        public ISet<UserEntity> Employees { get; protected set; } = new HashSet<UserEntity>();

        public RoomEntity(RoomType roomType, uint cost)
        {
            Id = Guid.NewGuid();
            RoomType = roomType;
            Cost = (int) cost;
        }
    }
}