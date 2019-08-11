using app.Domain.Entity.Token;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class TokenMap : ClassMap<Token>
    {
        public TokenMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.AccessToken).Column("AccessToken");
            Map(x => x.ExpiredAt).Column("ExpiredAt");
            References(x => x.User).Column("UserId");
        }
    }
}