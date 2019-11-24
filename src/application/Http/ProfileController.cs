using Application.CQS.Profile.Output;
using Application.CQS.Profile.Query;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController
    {
        [HttpGet]
        public ProfileOutput GetProfile([FromServices] GetProfileQuery query)
        {
            return query.Execute();
        }
    }
}
