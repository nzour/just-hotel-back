namespace app.Domain.Entity.Token
{
    public class Token : AbstractEntity
    {
        public virtual string AccessToken { get; protected internal set; }
        public virtual User.User User { get; protected internal set; }
        
        protected Token()
        {   
        }

        public Token(User.User user, string accessToken)
        {
            Identify();

            User = user;
            AccessToken = accessToken;
        }
    }
}