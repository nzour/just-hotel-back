namespace app.Application.CQS.Token.Output
{
    public class TokenOutput
    {
        public string AccessToken { get; }
        public string UserName { get; }

        public TokenOutput(Domain.Entity.Token.Token token)
        {
            AccessToken = token.AccessToken;
            UserName = token.User.Name;
        }
    }
}