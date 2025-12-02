using MediatR;
using OtoMangaStore.Application.DTOs.Auth;
namespace OtoMangaStore.Application.UseCases.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
