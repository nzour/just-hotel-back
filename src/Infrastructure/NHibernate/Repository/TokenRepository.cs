using app.Domain.Entity.Token;

namespace app.Infrastructure.NHibernate.Repository
{
    public class TokenRepository : AbstractRepository, ITokenRepository
    {
        public void Create(Token token)
        {
            Transactional.Action(() => Session.Save(token));
        }

        public Token FindToken(string accessToken)
        {
            return Session.QueryOver<Token>()
                .Where(token => token.AccessToken.Equals(accessToken))
                .SingleOrDefault();
        }
    }
}