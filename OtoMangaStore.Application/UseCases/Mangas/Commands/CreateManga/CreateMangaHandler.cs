using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.CreateManga
{
    public class CreateMangaHandler : IRequestHandler<CreateMangaCommand, MangaDto>
    {
        private readonly IUnitOfWork _uow;

        public CreateMangaHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<MangaDto> Handle(CreateMangaCommand request, CancellationToken cancellationToken)
        {
            var manga = new Content
            {
                Title = request.Title,
                Stock = request.Stock,
                Synopsis = request.Description,
                ImageUrl = request.CoverImageUrl,
                CategoryId = request.CategoryId,
                AuthorId = request.AuthorId
            };

            await _uow.Mangas.AddAsync(manga);
            await _uow.SaveChangesAsync();

            return new MangaDto
            {
                Id = manga.Id,
                Title = manga.Title,
                Stock = manga.Stock,
                Synopsis = manga.Synopsis,
                ImageUrl = manga.ImageUrl,
                CategoryId = manga.CategoryId,
                AuthorId = manga.AuthorId,
                CurrentPrice = 0
            };
        }
    }
}
