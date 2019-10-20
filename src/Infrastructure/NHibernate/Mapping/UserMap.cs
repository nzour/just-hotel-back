using App.Domain.UserEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).Column("Id");
            
            Map(x => x.FirstName).Column("FirstName").Not.Nullable();
            Map(x => x.LastName).Column("LastName").Not.Nullable();
            Map(x => x.Login).Column("Login").Not.Nullable().Unique();
            Map(x => x.Password).Column("Password").Not.Nullable();
        }
    }
}