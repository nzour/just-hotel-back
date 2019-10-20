namespace app.Application.CQS.Auth.Exception
{
    public class UserNotFound : System.Exception
    {
        private UserNotFound(string message) : base(message)
        {
        }

        public static UserNotFound InvalidCredentials()
        {
            return new UserNotFound("Invalid login or password.");
        }
    }
}