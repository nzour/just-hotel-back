using app.Domain.Entity.User;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Unique().Column("Name");
            Map(x => x.Login).Unique().Column("Login");
            Map(x => x.Password).Column("Password");
        }
    }
}