using app.Application.CQS.Token.Command;
using app.Application.CQS.Token.Input;
using app.Application.CQS.Token.Output;
using app.Common.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace app.Application.Http.Token
{
    [Route("token")]
    public class TokenController : Controller
    {
        [HttpPost]
        public TokenOutput Receive([FromBody] TokenInput input, [FromServices] ReceiveTokenCommand command)
        {
            return command.Execute(input);
        }

        [Route("test")]
        public void Test([FromServices] JwtTokenManager manager)
        {
            manager.GetCurrentUser();
        }
    }
}