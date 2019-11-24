namespace Application.CQS.User.Exception
{
    public class UpdatePasswordException : System.Exception
    {
        public UpdatePasswordException(string message): base(message)
        {
        }

        public static UpdatePasswordException InvalidOldPassword()
        {
            return new UpdatePasswordException("Invalid old password.");
        }
    }
}
