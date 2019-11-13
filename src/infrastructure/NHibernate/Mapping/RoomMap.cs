using Domain.Room;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infrastructure.NHibernate.Mapping
{
    public class RoomMap : ClassMap<RoomEntity>
    {
        public RoomMap()
        {
            Not.LazyLoad();

            Id(x => x.Id);
            Table("Rooms");

            Map(x => x.RoomType)
                .CustomType<EnumStringType<RoomType>>()
                .Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            HasMany(x => x.Employees);

            HasOne(x => x.Rent).Cascade.None();
        }
    }
}