namespace App.Application.CQS.Auth.Exception
{
    public class UserLoginIsBusyException : System.Exception
    {
        public UserLoginIsBusyException(string login): base($"User with login {login} already exists.")
        {
            
        }
    }
}