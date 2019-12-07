using Application.CQS.Room.Input;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Command
{
    public class CreateRoomCommand
    {
        private IEntityRepository<RoomEntity> RoomRepository { get; }

        public CreateRoomCommand(IEntityRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public void Execute(RoomInput input)
        {
            RoomRepository.SaveAndFlush(new RoomEntity(input.RoomType, input.Cost));
        }
    }
}