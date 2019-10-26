namespace Application.CQS.Auth.Input
{
    public class SignInInput
    {
        public SignInInput(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}