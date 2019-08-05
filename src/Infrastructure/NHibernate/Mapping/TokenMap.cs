using app.Domain.Entity.Token;
using FluentNHibernate.Mapping;

namespace app.Infrastructure.NHibernate.Mapping
{
    public class TokenMap : ClassMap<Token>
    {
        public TokenMap()
        {
            Id(x => x.Id);
            Map(x => x.AccessToken).Column("access_token");
            References(x => x.User);
        }
    }
}