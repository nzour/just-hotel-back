using Domain.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infrastructure.NHibernate.Mapping
{
    public class RoomMap : ClassMap<RoomEntity>
    {
        public RoomMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Table("Rooms");

            Map(x => x.RoomType)
                .CustomType<EnumStringType<RoomType>>()
                .Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            HasMany(x => x.Employees);

            HasMany(x => x.Transactions).KeyColumn("RoomId");
        }
    }
}
