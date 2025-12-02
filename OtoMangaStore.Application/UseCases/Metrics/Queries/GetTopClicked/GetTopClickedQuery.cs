using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Metrics.Queries.GetTopClicked
{
    public class GetTopClickedQuery : IRequest<IReadOnlyList<ClickTopDto>>
    {
        public int Top { get; }

        public GetTopClickedQuery(int top)
        {
            Top = top;
        }
    }
}
