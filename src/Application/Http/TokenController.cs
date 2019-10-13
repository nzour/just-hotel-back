using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Application.CQS.Token.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Application.Http
{
    [AllowAnonymous]
    [Route("token")]
    public class TokenController : Controller
    {
        [HttpPost]
        public TokenOutput ObtainToken([FromServices] ObtainTokenQuery query, [FromBody] TokenInput input)
        {
            return query.Execute(input);
        }
    }
}