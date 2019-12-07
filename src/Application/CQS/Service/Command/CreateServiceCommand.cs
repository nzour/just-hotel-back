using System.Threading.Tasks;
using Application.CQS.Service.Input;
using Domain;
using Domain.Entities;

namespace Application.CQS.Service.Command
{
    public class CreateServiceCommand
    {
        private IRepository<ServiceEntity> ServiceRepository { get; }

        public CreateServiceCommand(IRepository<ServiceEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public async Task Execute(ServiceInput input)
        {
            await ServiceRepository.SaveAndFlushAsync(input.ToEntity());
        }
    }
}
