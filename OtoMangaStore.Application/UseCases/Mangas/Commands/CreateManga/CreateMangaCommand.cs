using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.DTOs.Mangas;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga
{
    public class CreateMangaCommand : IRequest<MangaDto>
    {
        public CreateMangaDto MangaDto { get; set; }

        public CreateMangaCommand(CreateMangaDto mangaDto)
        {
            MangaDto = mangaDto;
        }
    }
}
