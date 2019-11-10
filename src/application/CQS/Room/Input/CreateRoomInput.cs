namespace Application.CQS.Room.Input
{
    public class CreateRoomInput
    {
        public string RoomType { get; set; }
        public int Cost { get; set; }

        public CreateRoomInput(string roomType, int cost)
        {
            RoomType = roomType;
            Cost = cost;
        }
    }
}