using Application.CQS.User.Exception;
using Application.CQS.User.Input;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;

namespace Application.CQS.User.Command
{
    public class UpdatePasswordCommand : IUserAware
    {
        public UserEntity? CurrentUser { get; set; }
        private IUserRepository UserRepository { get; }
        private IPasswordEncoder PasswordEncoder { get; }

        public UpdatePasswordCommand(IUserRepository userRepository, IPasswordEncoder passwordEncoder)
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
        }

        public void Execute(UpdatePasswordInput input)
        {
            var oldPassword = PasswordEncoder.Encrypt(input.OldPassword);

            if (oldPassword != CurrentUser!.Password)
            {
                throw UpdatePasswordException.InvalidOldPassword();
            }

            CurrentUser!.Password = PasswordEncoder.Encrypt(input.NewPassword);
        }
    }
}
