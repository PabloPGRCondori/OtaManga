using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Metrics.Commands.RegisterClick
{
    public class RegisterClickHandler : IRequestHandler<RegisterClickCommand>
    {
        private readonly IClickMetricsRepository _repo;

        public RegisterClickHandler(IClickMetricsRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(RegisterClickCommand request, CancellationToken cancellationToken)
        {
            await _repo.RegisterClickAsync(request.MangaId, request.UserId);
        }
    }
}
