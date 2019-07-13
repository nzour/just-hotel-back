using app.Application.CQS.User.Query;
using Microsoft.AspNetCore.Mvc;

namespace app.Application.Http.User
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("simple")]
        public JsonResult GetUser([FromServices] GetUserQuery query)
        {
            return Json(query.Execute());
        }
    }
}