using System;
using System.Threading.Tasks;
using Application.CQS.Service.Input;
using Domain;
using Domain.Service;

namespace Application.CQS.Service.Command
{
    public class UpdateServiceCommand
    {
        private IEntityRepository<ServiceEntity> ServiceRepository { get; }

        public UpdateServiceCommand(IEntityRepository<ServiceEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public async Task Execute(Guid serviceId, ServiceInput input)
        {
            var service = await ServiceRepository.GetAsync(serviceId);

            service.Name = input.Name;
            service.Cost = input.Cost;
        }
    }
}
