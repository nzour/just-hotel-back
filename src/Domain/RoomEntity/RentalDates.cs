using System;

namespace App.Domain.RoomEntity
{
    public class RentalDates
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public bool IsSomeEmpty => null == DateFrom || null == DateTo;
    }
}