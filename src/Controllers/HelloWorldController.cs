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
        public ActionResult<string> HelloWorld([FromRoute] string name)
        {
            var response = new HelloWorldDto();
            
            try
            {
                response.Message = "Success!";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return Json(response);
        }
    }
}