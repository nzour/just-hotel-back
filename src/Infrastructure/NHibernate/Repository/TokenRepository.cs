using app.Domain.Entity.Token;

namespace app.Infrastructure.NHibernate.Repository
{
    public class TokenRepository : AbstractRepository, ITokenRepository
    {
        public TokenRepository(Transactional transactional) : base(transactional)
        {
        }

        public void Create(Token token)
        {
            Transactional.Action(session => session.Save(token));
        }

        public Token FindToken(string accessToken)
        {
            return Transactional.WithSession(session =>
            {
                return session.QueryOver<Token>()
                    .Where(token => token.AccessToken == accessToken)
                    .SingleOrDefault();
            });
        }
    }
}