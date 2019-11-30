using System;
using Domain.Entities;

namespace Application.CQS.Service.Output
{
    public class ServiceOutput
    {
        public Guid Id { get; }
        public string Name { get; }
        public uint Cost { get; }

        public ServiceOutput(ServiceEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Cost = (uint) entity.Cost;
        }
    }
}
