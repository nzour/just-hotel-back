using System;
using Application.CQS.Room.Command;
using Application.CQS.Room.Input;
using Application.CQS.Room.Output;
using Application.CQS.Room.Query;
using Common.Util;
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("rooms")]
    public class RoomController : Controller
    {
        [HttpPost]
        [AuthorizeRoles(UserRole.Manager)]
        public void CreateRoom([FromServices] CreateRoomCommand command, [FromBody] RoomInput input)
        {
            command.Execute(input);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{roomId}")]
        public RoomOutput GetRoom([FromServices] GetRoomQuery query, Guid roomId)
        {
            return query.Execute(roomId);
        }

        [HttpGet]
        [AllowAnonymous]
        public PaginatedData<RoomOutput> GetAllRooms(
            [FromServices] GetAllRoomsQuery query,
            [FromQuery] GetRoomInputFilter filter, [FromQuery] Pagination pagination
        )
        {
            return query.Execute(filter, pagination);
        }

        [HttpPut]
        [Route("{roomId}")]
        public void UpdateRoom(
            [FromServices] UpdateRoomCommand command, [FromBody] RoomInput input,
            [FromRoute] Guid roomId
        )
        {
            command.Execute(roomId, input);
        }
    }
}
