using Application.CQS.Profile.Output;
using Application.CQS.Profile.Query;
using Application.CQS.Rent.Output;
using Application.CQS.Rent.Query;
using Common.Util;
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

        [HttpGet]
        [Route("rents")]
        public PaginatedData<RentOutput> GetOwnRents(
            [FromServices] GetCurrentUserRentsQuery query,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(pagination);
        }
    }
}
