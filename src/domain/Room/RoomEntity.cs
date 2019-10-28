using System;
using System.Collections.Generic;

namespace Domain.Room
{
    public class RoomEntity : AbstractEntity
    {
        public RoomType RoomType { get; internal set; } = RoomType.Single;
        public ISet<User.UserEntity> Employees { get; internal set; } = new HashSet<User.UserEntity>();
        public int Cost { get; internal set; }
        public User.UserEntity? RentedBy { get; internal set; }
        public RentalDates RentalDates { get; internal set; } = new RentalDates();
        public bool IsRented => null != RentedBy;

        protected RoomEntity()
        {
        }

        public RoomEntity(RoomType type, int cost)
        {
            Identify();

            RoomType = type;
            Cost = cost;

            RentalDates = new RentalDates();
        }
    }

    #region types

    public class RentalDates
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool IsSomeEmpty => null == DateFrom || null == DateTo;
    }

    public enum RoomType
    {
        Single,
        Double,
        Triple
    }

    #endregion
}