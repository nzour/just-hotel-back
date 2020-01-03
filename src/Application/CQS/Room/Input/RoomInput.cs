using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.CQS.Room.Input
{
    public class RoomInput
    {
        [Required]
        public uint Cost { get; set; }

        [Required]
        public RoomType RoomType { get; set; }
        public IEnumerable<string> Images { get; set; }

        public RoomInput(uint cost, RoomType roomType, IEnumerable<string> images)
        {
            RoomType = roomType;
            Cost = cost;
            Images = images;
        }
    }
}
