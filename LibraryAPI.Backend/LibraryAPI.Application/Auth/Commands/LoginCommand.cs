using LibraryAPI.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace LibraryAPI.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }


        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByUsernameAndPassword(request.UserName, request.Password);
            if (user == null)
            {
                return null; // Верни null или ошибку в зависимости от логики
            }

            var token = _tokenService.GenerateToken(user.Id.ToString(), user.Role);

            return new LoginResponse { Token = token };
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
