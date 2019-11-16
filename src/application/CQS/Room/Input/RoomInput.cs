using System.ComponentModel.DataAnnotations;
using Domain.Room;

namespace Application.CQS.Room.Input
{
    public class RoomInput
    {
        [Required]
        public uint Cost { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        public RoomInput(uint cost, RoomType roomType)
        {
            RoomType = roomType;
            Cost = cost;
        }
    }
}
