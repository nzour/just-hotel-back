using Application.CQS.Room.Input;
using Domain.RoomEntity;
using _Room = Domain.RoomEntity.Room;

namespace Application.CQS.Room.Command
{
    public class CreateRoomCommand
    {
        public IRoomRepository RoomRepository { get; }

        public CreateRoomCommand(IRoomRepository roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public void Execute(CreateRoomInput input)
        {
            RoomRepository.SaveAsync(new _Room(input.Type, input.Cost));
        }
    }
}