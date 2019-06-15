using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace
{
    [ApiController][Route("api")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet("hello-world")]
        public ActionResult<string> HelloWorld()
        {
            return "Hello world!";
        }
    }
}