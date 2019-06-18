using app.Modules.Dto;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api")]
    public class HelloWorldController : Controller
    {
        [HttpGet("hello-world/{name}")]
        public ActionResult<string> HelloWorld([FromRoute] string name)
        {
            string message = $"Hello {name}! It works!";

            return Json(new HelloWorldDto(message));
        }
    }
}