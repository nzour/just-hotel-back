using System.Linq;
using Application.CQS.Room.Input;
using Application.CQS.Room.Output;
using Common.Extensions;
using Common.Util;
using Domain.Room;

namespace Application.CQS.Room.Query
{
    public class GetAllRoomsQuery
    {
        private IRoomRepository RoomRepository { get; }

        public GetAllRoomsQuery(IRoomRepository roomRepository)
        {
            RoomRepository = roomRepository;
        }

        public PaginatedData<RoomOutput> Execute(GetRoomInputFilter filter, Pagination pagination)
        {
            return RoomRepository.FindAll()
                .ApplyFilter(filter)
                .Select(room => new RoomOutput(room))
                .Paginate(pagination);
        }
    }
}