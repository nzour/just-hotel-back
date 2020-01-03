using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.CQS.Room.Output
{
    public class RoomOutput
    {
        public Guid Id { get; set; }
        public uint Cost { get; set; }
        public RoomType RoomType { get; set; }
        public IEnumerable<string> Images { get; set; }

        public RoomOutput(RoomEntity room)
        {
            Id = room.Id;
            Cost = (uint) room.Cost;
            RoomType = room.RoomType;
            Images = room.Images;
        }
    }
}
