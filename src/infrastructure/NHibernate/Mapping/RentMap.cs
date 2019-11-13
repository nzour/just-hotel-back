using Domain.Rent;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class RentMap : ClassMap<RentEntity>
    {
        public RentMap()
        {
            Id(x => x.Id).GeneratedBy.Foreign("Room");
            Table("Rents");

            HasOne(x => x.Room).Constrained();

            HasManyToMany(x => x.Services)
                .ParentKeyColumn("RentId")
                .ChildKeyColumn("ServiceId")
                .Table("ServiceRent");

            References(x => x.User)
                .Column("UserId")
                .Not.Nullable();

            Map(x => x.StartedAt)
                .Not.Nullable();

            Map(x => x.ExpiredAt)
                .Not.Nullable();
        }
    }
}