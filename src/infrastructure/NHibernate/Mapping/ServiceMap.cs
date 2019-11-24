using Domain.Service;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class ServiceMap : ClassMap<ServiceEntity>
    {
        public ServiceMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Table("Services");

            Map(x => x.Name).Not.Nullable();
            Map(x => x.Cost).Not.Nullable();
        }
    }
}