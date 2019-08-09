using System;
using System.Threading.Tasks;
using app.Domain.Entity.Token;
using Microsoft.AspNetCore.Mvc;

namespace app.Application.Http.Token
{
    [ApiController]
    [Route("token")]
    public class TokenController : Controller
    {
        public async Task<string> Index()
        {
//            throw new TokenException("Something went wrong");
            return await Task.Run(() => "Ok");
        }
    }
}