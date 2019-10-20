using app.Domain.User;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).Column("Id");
            Map(x => x.FirstName).Column("FirstName");
            Map(x => x.LastName).Column("LastName");
            Map(x => x.Login).Column("Login").Unique();
            Map(x => x.Password).Column("Password");
        }
    }
}