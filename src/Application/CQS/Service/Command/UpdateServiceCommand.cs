using System;
using System.Threading.Tasks;
using Application.CQS.Service.Input;
using Domain;
using Domain.Entities;

namespace Application.CQS.Service.Command
{
    public class UpdateServiceCommand
    {
        private IRepository<ServiceEntity> ServiceRepository { get; }

        public UpdateServiceCommand(IRepository<ServiceEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public async Task Execute(Guid serviceId, ServiceInput input)
        {
            var service = await ServiceRepository.GetAsync(serviceId);

            service.Name = input.Name;
            service.Cost = (int) input.Cost;
        }
    }
}
