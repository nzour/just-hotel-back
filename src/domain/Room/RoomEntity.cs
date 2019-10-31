using System.Collections.Generic;
using Domain.User;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public RoomType RoomType { get; internal set; } = RoomType.Single;
        public ISet<UserEntity> Employees { get; internal set; } = new HashSet<UserEntity>();
        public int Cost { get; internal set; }

        protected RoomEntity()
        {
        }

        public RoomEntity(RoomType type, int cost)
        {
            Identify();

            RoomType = type;
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