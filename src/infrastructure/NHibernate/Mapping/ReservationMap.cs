using Domain.Reservation;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class ReservationMap : ClassMap<ReservationEntity>
    {
        public ReservationMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Table("Reservations");

            Map(x => x.ReservedFrom)
                .Not.Nullable();

            Map(x => x.ReservedTo)
                .Not.Nullable();
            
            References(x => x.User, "UserId")
                .Cascade.Delete()
                .Not.Nullable();

            References(x => x.Room, "RoomId")
                .Cascade.Delete()
                .Not.Nullable();
        }
    }
}
