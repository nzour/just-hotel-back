using System;
using Application.CQS.User.Output;
using Application.CQS.User.Query;
using Common.Util;
using Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [Route("users")]
    [Authorize(Roles = UserRole.Manager)]
    public class UserController : Controller
    {
        public PaginatedData<UserOutput> GetAllUsers([FromServices] GetAllUsersQuery query,
            [FromQuery] Pagination pagination)
        {
            return query.Execute(pagination);
        }

        [Route("{userId}")]
        public UserOutput GetUser([FromServices] GetUserQuery query, [FromRoute] Guid userId)
        {
            return query.Execute(userId);
        }
    }
}