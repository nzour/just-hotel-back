using System.Collections.Generic;
using Domain.Rent;
using Domain.User;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public RoomType RoomType { get; set; }
        public uint Cost { get; set; }
        public ISet<UserEntity> Employees { get; set; } = new HashSet<UserEntity>();
        public RentEntity? Rent { get; protected set; }
        public bool IsRented => null != Rent;

        public RoomEntity(RoomType roomType, uint cost)
        {
            Identify();

            RoomType = roomType;
            Cost = cost;
        }
    }

    public enum RoomType
    {
        Single,
        Double,
        Triple
    }
}