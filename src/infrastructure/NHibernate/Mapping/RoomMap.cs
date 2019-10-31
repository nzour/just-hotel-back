using Domain.Room;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class RoomMap : ClassMap<RoomEntity>
    {
        public RoomMap()
        {
            Not.LazyLoad();

            Id(x => x.Id);
            Table("Rooms");

            Map(x => x.RoomType).CustomType<RoomType>().Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            HasMany(x => x.Employees);
        }
    }
}