#nullable disable

namespace Application.CQS.Auth.Input
{
    public class SignUpInput
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}