using Domain.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;
using NHibernate.UserTypes;

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
            Map(x => x.Images)
                .CustomType<JsonArrayType<string[]>>()
                .Not.Nullable();

            HasMany(x => x.Employees);
        }

        public void Kek(IUserType type)
        {
            
        }
    }
}
