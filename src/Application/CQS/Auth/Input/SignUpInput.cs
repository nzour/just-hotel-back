using System.ComponentModel.DataAnnotations;

#nullable disable

namespace app.Application.CQS.Auth.Input
{
    public class SignUpInput
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}