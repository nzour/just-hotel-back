namespace Application.CQS.Auth.Input
{
    public class SignUpInput
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public SignUpInput(string login, string firstName, string lastName, string password)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}