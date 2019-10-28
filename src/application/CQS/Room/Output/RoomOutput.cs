using System;
using System.Collections.Generic;
using System.Linq;
using Application.CQS.User.Output;
using Domain.Room;

#nullable disable

namespace Application.CQS.Room.Output
{
    public class RoomOutput
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public int Cost { get; set; }
        public bool IsRented { get; set; }
        public UserOutput? RentedBy { get; set; }
        public RentalDates RentalDates { get; set; }
        public IEnumerable<UserOutput> Employees { get; set; }

        public static implicit operator RoomOutput(RoomEntity room)
        {
            return new RoomOutput
            {
                Id = room.Id,
                RoomType = room.RoomType,
                Cost = room.Cost,
                IsRented = room.IsRented,
                RentedBy = room.RentedBy,
                RentalDates = room.RentalDates,
                Employees = room.Employees.Select(e => (UserOutput) e)
            };
        }
    }
}