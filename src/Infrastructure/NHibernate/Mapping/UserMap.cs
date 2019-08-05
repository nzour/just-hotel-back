using app.Domain.Entity.User;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Login);
            Map(x => x.Password);
        }
    }
}