using System.ComponentModel.DataAnnotations;

#nullable disable

namespace app.Application.CQS.Token.Input
{
    public class TokenInput
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}