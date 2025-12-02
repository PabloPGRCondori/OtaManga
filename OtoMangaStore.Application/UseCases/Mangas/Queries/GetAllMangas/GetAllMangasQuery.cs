using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Mangas.Queries.GetAllMangas
{
    public class GetAllMangasQuery : IRequest<IEnumerable<MangaDto>>
    {
    }
}
