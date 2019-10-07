namespace app.Domain.Token
{
    public interface ITokenRepository
    {
        void Create(Token token);

        Token FindToken(string accessToken);
    }
}