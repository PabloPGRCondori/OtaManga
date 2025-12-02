using MediatR;

namespace OtoMangaStore.Application.UseCases.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<bool>
    {
    }
}
