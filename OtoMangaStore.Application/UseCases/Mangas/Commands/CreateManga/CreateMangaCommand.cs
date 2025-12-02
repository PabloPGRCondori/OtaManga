using MediatR;
using OtoMangaStore.Application.DTOs;


namespace OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga
{
    public class CreateMangaCommand : IRequest<MangaDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
