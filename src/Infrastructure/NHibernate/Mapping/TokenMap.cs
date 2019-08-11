using app.Domain.Entity.Token;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class TokenMap : ClassMap<Token>
    {
        public TokenMap()
        {
            Table("tokens");
            Id(x => x.Id).Column("id");
            Map(x => x.AccessToken).Column("access_token");
            Map(x => x.ExpiredAt).Column("expired_at");
            References(x => x.User).Column("user_id");
        }
    }
}