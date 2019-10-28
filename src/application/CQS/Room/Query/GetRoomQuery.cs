using System;
using Application.CQS.Room.Output;
using Domain;
using Domain.Room;

namespace Application.CQS.Room.Query
{
    public class GetRoomQuery
    {
        private IEntityRepository<RoomEntity> RoomRepository { get; }

        public GetRoomQuery(IEntityRepository<RoomEntity> roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public RoomOutput Execute(Guid roomId)
        {
            return RoomRepository.Get(roomId);
        }
    }
}