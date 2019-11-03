using System.Collections.Generic;
using System.Linq;
using Domain.Rent;
using Domain.User;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public string RoomType { get; set; } = RoomTypes.Single;
        public ISet<UserEntity> Employees { get; internal set; } = new HashSet<UserEntity>();
        public int Cost { get; internal set; }
        public RentEntity? Rent { get; private set; }
        public bool IsBusy => null != Rent;

        protected RoomEntity()
        {
        }

        public RoomEntity(string roomType, int cost)
        {
            RoomTypes.Validate(roomType);
            
            Identify();

            RoomType = roomType;
            Cost = cost;
        }
    }

    public static class RoomTypes
    {
        public static string Single { get; } = "Single";
        public static string Double { get; } = "Double";
        public static string Triple { get; } = "Triple";

        public static IEnumerable<string> ValidTypes { get; } = new[] { Single, Double, Triple };

        public static void Validate(string roomType)
        {
            if (ValidTypes.Contains(roomType))
            {
                return;
            }
            
            throw RoomException.InvalidRoomType(roomType);
        }
    }
}