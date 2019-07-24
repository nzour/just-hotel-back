using Microsoft.AspNetCore.Mvc;

namespace app.Application.Http.Token
{
    [ApiController]
    [Route("token")]
    public class TokenController : Controller
    {
        [Route("index")]
        public JsonResult Index()
        {
            return Json("OK");
        }
    }
}