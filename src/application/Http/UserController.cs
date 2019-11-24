using System;
using Application.CQS.User.Command;
using Application.CQS.User.Input;
using Application.CQS.User.Output;
using Application.CQS.User.Query;
using Common.Util;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("users")]
    [AuthorizeRoles(UserRole.Manager)]
    public class UserController : Controller
    {
        [HttpGet]
        public PaginatedData<UserOutput> GetAllUsers(
            [FromServices] GetAllUsersQuery query,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(pagination);
        }

        [HttpGet]
        [Route("{userId}")]
        public UserOutput GetUser([FromServices] GetUserQuery query, [FromRoute] Guid userId)
        {
            return query.Execute(userId);
        }

        [HttpPut("update-names")]
        public void UpdateUserNames([FromServices] UpdateNamesCommand command, [FromBody] UpdateNamesInput input)
        {
            command.Execute(input);
        }

        [HttpPut("update-password")]
        public void UpdateUserPassword([FromServices] UpdatePasswordCommand command, [FromBody] UpdatePasswordInput input)
        {
            command.Execute(input);
        }
    }
}
