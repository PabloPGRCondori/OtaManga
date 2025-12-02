using MediatR;
using OtoMangaStore.Application.DTOs.Auth;

namespace OtoMangaStore.Application.UseCases.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public LoginRequestDto Request { get; }

        public LoginCommand(LoginRequestDto request)
        {
            Request = request;
        }
    }
}
