using System.Collections.Generic;

namespace Domain.Entities
{
    public enum RoomType
    {
        Single,
        Double,
        Triple
    }

    public class RoomEntity : AbstractEntity
    {
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }
        public ISet<UserEntity> Employees { get; protected set; } = new HashSet<UserEntity>();
        public ISet<TransactionEntity> Transactions { get; protected set; } = new HashSet<TransactionEntity>();

        public RoomEntity(RoomType roomType, uint cost)
        {
            Identify();

            RoomType = roomType;
            Cost = (int) cost;
        }
    }
}