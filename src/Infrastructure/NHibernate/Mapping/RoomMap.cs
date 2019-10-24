using App.Domain.RoomEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class RoomMap : ClassMap<Room>
    {
        public RoomMap()
        {
            Not.LazyLoad();

            Id(x => x.Id);
            Table("Rooms");

            Map(x => x.RoomType).CustomType<RoomType>().Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            References(x => x.RentedBy).Column("RentedById").Nullable();
            HasMany(x => x.Employees);

            Component(x => x.RentalDates, embedded =>
            {
                embedded.Map(date => date.DateFrom).Column("RentalFrom").Nullable();
                embedded.Map(date => date.DateTo).Column("RentalTo").Nullable();
            });
        }
    }
}