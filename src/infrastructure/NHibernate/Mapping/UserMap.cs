using Domain.User;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<UserEntity>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id);

            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Login).Not.Nullable().Unique();
            Map(x => x.Password).Not.Nullable();

            Map(x => x.Role)
                .CustomType<EnumStringType<UserRole>>()
                .Not.Nullable();

            HasMany(x => x.Rents).KeyColumn("UserId");
        }
    }
}