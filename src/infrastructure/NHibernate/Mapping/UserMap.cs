using Domain.User;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<UserEntity>
    {
        public UserMap()
        {
            Not.LazyLoad();

            Table("Users");
            Id(x => x.Id);

            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.Login).Not.Nullable().Unique();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.Role).Not.Nullable();

            HasMany(x => x.Rents).KeyColumn("UserId");
        }
    }
}