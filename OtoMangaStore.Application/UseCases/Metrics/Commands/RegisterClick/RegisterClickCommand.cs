using MediatR;

namespace OtoMangaStore.Application.UseCases.Metrics.Commands.RegisterClick
{
    public class RegisterClickCommand : IRequest
    {
        public int MangaId { get; }
        public string UserId { get; }

        public RegisterClickCommand(int mangaId, string userId)
        {
            MangaId = mangaId;
            UserId = userId;
        }
    }
}
