namespace Application.CQS.User.Input
{
    public class UpdateAvatarInput
    {
        public string Avatar { get; set; }

        public UpdateAvatarInput(string avatar)
        {
            Avatar = avatar;
        }
    }
}
