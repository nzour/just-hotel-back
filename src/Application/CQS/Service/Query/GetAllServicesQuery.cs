using System.Linq;
using Application.CQS.Service.Output;
using Common.Extensions;
using Common.Util;
using Domain;
using Domain.Entities;

namespace Application.CQS.Service.Query
{
    public class GetAllServicesQuery
    {
        private IEntityRepository<ServiceEntity> ServiceRepository { get; }

        public GetAllServicesQuery(IEntityRepository<ServiceEntity> serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public PaginatedData<ServiceOutput> GetAllServices(Pagination pagination)
        {
            return ServiceRepository.FindAll()
                .Select(s => new ServiceOutput(s))
                .Paginate(pagination);
        }
    }
}
