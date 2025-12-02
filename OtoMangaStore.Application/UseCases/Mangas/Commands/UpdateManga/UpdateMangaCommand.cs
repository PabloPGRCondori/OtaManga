using MediatR;
using OtoMangaStore.Application.DTOs.Mangas;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.UpdateManga
{
    public class UpdateMangaCommand : IRequest
    {
        public UpdateMangaDto MangaDto { get; set; }

        public UpdateMangaCommand(UpdateMangaDto mangaDto)
        {
            MangaDto = mangaDto;
        }
    }
}
