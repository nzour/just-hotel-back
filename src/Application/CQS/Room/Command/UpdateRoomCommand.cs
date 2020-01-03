using System;
using System.Linq;
using System.Threading.Tasks;
using Application.CQS.Room.Input;
using Domain;
using Domain.Entities;

namespace Application.CQS.Room.Command
{
    public class UpdateRoomCommand
    {
        private IRepository<RoomEntity> RoomRepository { get; }

        public UpdateRoomCommand(IRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public async Task ExecuteAsync(Guid roomId, RoomInput input)
        {
            var room = await RoomRepository.GetAsync(roomId);

            room.Cost = (int) input.Cost;
            room.RoomType = input.RoomType;
            room.Images = input.Images.ToList();
        }
    }
}