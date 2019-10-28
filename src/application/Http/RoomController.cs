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
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public void CreateRoom([FromServices] CreateRoomCommand command, [FromBody] CreateRoomInput input)
        {
            command.Execute(input);
        }

        [Route("{roomId}")]
        public RoomOutput GetRoom([FromServices] GetRoomQuery query, Guid roomId)
        {
            return query.Execute(roomId);
        }

        [Route("rented")]
        public PaginatedData<RoomShortOutput> GetRentedRooms(
            [FromServices] GetRentedRoomsQuery query,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(pagination);
        }

        [Route("not-rented")]
        public PaginatedData<RoomShortOutput> GetNotRentedRooms(
            [FromServices] GetNotRentedRoomsQuery query,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(pagination);
        }
    }
}