using System.ComponentModel.DataAnnotations;

namespace app.Application.CQS.Auth.Input
{
    public class SignInInput
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        public SignInInput(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}