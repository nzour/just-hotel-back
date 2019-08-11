using System;
using app.Application.CQS.User.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace app.Application.Http.User
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("{userId}")]
        public JsonResult GetUser([FromRoute] Guid userId, [FromServices] GetUserQuery query)
        {
            return Json(query.Execute(userId));
        }
    }
}