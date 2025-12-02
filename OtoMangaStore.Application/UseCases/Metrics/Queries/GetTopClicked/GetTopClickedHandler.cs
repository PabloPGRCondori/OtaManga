using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Metrics.Queries.GetTopClicked
{
    public class GetTopClickedHandler : IRequestHandler<GetTopClickedQuery, IReadOnlyList<ClickTopDto>>
    {
        private readonly IClickMetricsRepository _repo;

        public GetTopClickedHandler(IClickMetricsRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<ClickTopDto>> Handle(GetTopClickedQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetTopClickedAsync(request.Top);
        }
    }
}
