using System;
using app.CQS.User.Command;
using app.CQS.User.Input;
using app.CQS.User.Query;
using app.Modules.ORM.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("index")]
        public JsonResult Index()
        {
            return Json("It works!");
        }

        [HttpPost]
        public void User(UserInput input, [FromServices] CreateUserCommand command)
        {
            command.Execute(input);
        }

        [HttpGet]
        [Route("{user_id}")]
        public JsonResult GetUser([FromRoute] Guid userId, [FromServices] GetUserQuery query)
        {
            return Json(query.Execute(userId));
        }

        [HttpGet]
        [Route("demo")]
        public JsonResult GetDemo([FromServices] IDemoRepository demoRepository)
        {
            return Json(demoRepository.Provide());
        }
    }
}