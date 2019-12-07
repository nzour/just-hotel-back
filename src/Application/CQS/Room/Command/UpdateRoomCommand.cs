using System;
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

        public void Execute(Guid roomId, RoomInput input)
        {
            var room = RoomRepository.Get(roomId);

            room.Cost = (int) input.Cost;
            room.RoomType = input.RoomType;
        }
    }
}