namespace app.Application.CQS.Token.Exception
{
    public class UserNotFound : System.Exception
    {
        public UserNotFound(string message) : base(message)
        {
        }

        public static UserNotFound InvalidCredentials()
        {
            return new UserNotFound("Invalid login or password.");
        }
    }
}