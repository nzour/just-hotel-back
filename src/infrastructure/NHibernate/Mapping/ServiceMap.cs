using Domain.Service;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class ServiceMap : ClassMap<ServiceEntity>
    {
        public ServiceMap()
        {
            Table("Services");

            Id(x => x.Id).GeneratedBy.Assigned();

            Map(x => x.Name).Not.Nullable();
            Map(x => x.Cost).Not.Nullable();
        }
    }
}