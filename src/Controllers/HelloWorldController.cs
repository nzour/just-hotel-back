using System;
using System.Data.SqlClient;
using app.Modules.Dto;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api")]
    public class HelloWorldController : Controller
    {
        [HttpGet("hello-world/{name}")]
        public JsonResult HelloWorld([FromRoute] string name)
        {
            var response = new HelloWorldDto();
            response.Message = $"Hello {name}";

            return Json(response);
        }
    }
}