using System.Collections.Generic;
using Domain.Rent;
using Domain.User;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public RoomType RoomType { get; internal set; }
        public int Cost { get; internal set; }
        public ISet<UserEntity> Employees { get; internal set; } = new HashSet<UserEntity>();
        public RentEntity? Rent { get; private set; }
        public bool IsRented => null != Rent;

        public RoomEntity(RoomType roomType, int cost)
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