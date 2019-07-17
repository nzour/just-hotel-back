using app.Infrastructure.NHibernate;
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
            var foo = new Foo();
            return Json(DbAccessor.ConnectionString);
        }
    }
}