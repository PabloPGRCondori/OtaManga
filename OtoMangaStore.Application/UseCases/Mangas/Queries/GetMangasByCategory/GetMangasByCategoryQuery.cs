using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangasByCategory
{
    public class GetMangasByCategoryQuery : IRequest<IEnumerable<MangaDto>>
    {
        public int CategoryId { get; set; }

        public GetMangasByCategoryQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
