using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById
{
    public class GetMangaByIdQuery : IRequest<MangaDto?>
    {
        public int Id { get; set; }

        public GetMangaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
