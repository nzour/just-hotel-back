using System;
using Application.CQS.User.Output;
using Application.CQS.User.Query;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [Route("users")]
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