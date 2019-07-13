using FluentNHibernate.Mapping;

namespace app.Domain.Mapping.User
{
    public class UserMap : ClassMap<Entity.User.User>
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