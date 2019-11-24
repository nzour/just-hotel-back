using Application.CQS.User.Input;
using Domain.Entities;

namespace Application.CQS.User.Command
{
    public class UpdateNamesCommand : IUserAware
    {
        public UserEntity CurrentUser { get; set; }

        public void Execute(UpdateNamesInput input)
        {
            CurrentUser!.FirstName = input.FirstName;
            CurrentUser!.LastName = input.LastName;
        }
    }
}
