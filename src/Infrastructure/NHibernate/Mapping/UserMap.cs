using app.Domain.Entity.User;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("users");
            Id(x => x.Id).Column("id");
            Map(x => x.Name).Unique().Column("name");
            Map(x => x.Login).Unique().Column("login");
            Map(x => x.Password).Column("password");
        }
    }
}