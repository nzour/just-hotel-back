namespace app.Domain.Entity.Token
{
    public interface ITokenRepository
    {
        void Create(Token token);

        bool IsExpired(string accessToken);
    }
}