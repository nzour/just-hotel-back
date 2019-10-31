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
        public IEnumerable<UserOutput> Employees { get; set; }

        public RoomOutput(RoomEntity room)
        {
            Id = room.Id;
            RoomType = room.RoomType;
            Cost = room.Cost;
            Employees = room.Employees.Select(e => new UserOutput(e));
        }
    }
}