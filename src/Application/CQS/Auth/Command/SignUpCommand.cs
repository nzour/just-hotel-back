using System.Threading.Tasks;
using Application.CQS.Auth.Exception;
using Application.CQS.Auth.Input;
using Application.CQS.Auth.Output;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;

namespace Application.CQS.Auth.Command
{
    public class SignUpCommand
    {
        private IUserRepository UserRepository { get; }

        private IPasswordEncoder PasswordEncoder { get; }
        private IJwtTokenService TokenService { get; }

        public SignUpCommand(IUserRepository userRepository, IPasswordEncoder passwordEncoder, IJwtTokenService tokenService)
        {
            UserRepository = userRepository;
            PasswordEncoder = passwordEncoder;
            TokenService = tokenService;
        }

        public async Task<SignInOutput> ExecuteAsync(SignUpInput input)
        {
            await AssertLoginIsNotBusyAsync(input.Login);

            var encryptedPassword = PasswordEncoder.Encrypt(input.Password);
            var user = new UserEntity(input.FirstName, input.LastName, input.Login, encryptedPassword, UserRole.Client);

            await UserRepository.SaveAndFlushAsync(user);

            return new SignInOutput(user, TokenService.CreateToken(user));
        }

        private async Task AssertLoginIsNotBusyAsync(string login)
        {
            if (await UserRepository.IsLoginBusyAsync(login))
            {
                throw new UserLoginIsBusyException(login);
            }
        }
    }
}