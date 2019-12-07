using System;
using System.Threading.Tasks;
using Application.CQS.Room.Command;
using Application.CQS.Room.Input;
using Application.CQS.Room.Output;
using Application.CQS.Room.Query;
using Common.Util;
using Domain.Entities;
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
        public async Task CreateRoom([FromServices] CreateRoomCommand command, [FromBody] RoomInput input)
        {
            await command.ExecuteAsync(input);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{roomId}")]
        public async Task<RoomOutput> GetRoom([FromServices] GetRoomQuery query, Guid roomId)
        {
            return await query.ExecuteAsync(roomId);
        }

        [HttpGet]
        [AllowAnonymous]
        public PaginatedData<RoomOutput> GetAllRooms(
            [FromServices] GetAllRoomsQuery query,
            [FromQuery] RoomsFilter filter, [FromQuery] Pagination pagination
        )
        {
            return query.Execute(filter, pagination);
        }

        [HttpPut]
        [Route("{roomId}")]
        public async Task UpdateRoom(
            [FromServices] UpdateRoomCommand command, [FromBody] RoomInput input,
            [FromRoute] Guid roomId
        )
        {
            await command.ExecuteAsync(roomId, input);
        }
    }
}
