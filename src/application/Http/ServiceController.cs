using System;
using Application.CQS.Service.Command;
using Application.CQS.Service.Input;
using Application.CQS.Service.Output;
using Application.CQS.Service.Query;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("services")]
    public class ServiceController
    {
        [HttpPost]
        public async void CreateService([FromServices] CreateServiceCommand command, [FromBody] ServiceInput input)
        {
            await command.Execute(input);
        }

        [HttpPut]
        public async void UpdateService(
            [FromServices] UpdateServiceCommand command,
            [FromBody] ServiceInput input,
            [FromRoute] Guid serviceId
        )
        {
            await command.Execute(serviceId, input);
        }

        [HttpGet]
        public PaginatedData<ServiceOutput> GetAllServices(
            [FromServices] GetAllServicesQuery query,
            [FromQuery] Pagination pagination
        )
        {
            return query.GetAllServices(pagination);
        }
    }
}
