using System.Threading.Tasks;
using Application.CQS.Service.Input;
using Domain;
using Domain.Service;

namespace Application.CQS.Service.Command
{
    public class CreateServiceCommand
    {
        private IEntityRepository<ServiceEntity> ServiceRepository { get; }

        public CreateServiceCommand(IEntityRepository<ServiceEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public async Task Execute(ServiceInput input)
        {
            await ServiceRepository.SaveAndFlushAsync(input.ToEntity());
        }
    }
}
