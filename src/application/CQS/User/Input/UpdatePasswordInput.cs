namespace Application.CQS.User.Input
{
    public class UpdatePasswordInput
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public UpdatePasswordInput(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
