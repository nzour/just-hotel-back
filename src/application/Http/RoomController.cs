using System;
using Application.CQS.Room.Command;
using Application.CQS.Room.Input;
using Application.CQS.Room.Output;
using Application.CQS.Room.Query;
using Common.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [Route("rooms")]
    public class RoomController : Controller
    {
        // POST /rooms
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public void CreateRoom([FromServices] CreateRoomCommand command, [FromBody] CreateRoomInput input)
        {
            command.Execute(input);
        }

        // GET /rooms/{roomId}
        [Route("{roomId}")]
        public RoomOutput GetRoom([FromServices] GetRoomQuery query, Guid roomId)
        {
            return query.Execute(roomId);
        }

        // GET /rooms
        public PaginatedData<RoomOutput> GetAllRooms([FromServices] GetAllRoomsQuery query,
            [FromQuery] GetRoomInputFilter filter, [FromQuery] Pagination pagination)
        {
            return query.Execute(filter, pagination);
        }
    }
}