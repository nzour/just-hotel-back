using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using Domain.Rent;
using Domain.User;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public string RoomType { get; internal set; }
        public ISet<UserEntity> Employees { get; internal set; } = new HashSet<UserEntity>();
        public int Cost { get; internal set; }
        public RentEntity? Rent { get; private set; }
        public bool IsRented => null != Rent;

        public RoomEntity(string roomType, int cost)
        {
            roomType.AssertValidRoomType();

            Identify();

            RoomType = roomType;
            Cost = cost;
        }
    }

    public static class RoomTypes
    {
        public const string Single = "Single";
        public const string Double = "Double";
        public const string Triple = "Triple";

        public static IEnumerable<string> ValidTypes { get; } = new[] { Single, Double, Triple };

        public static void AssertValidRoomType(this string roomType)
        {
            if (ValidTypes.Contains(roomType))
            {
                return;
            }

            throw RoomException.InvalidRoomType(roomType);
        }
    }
}