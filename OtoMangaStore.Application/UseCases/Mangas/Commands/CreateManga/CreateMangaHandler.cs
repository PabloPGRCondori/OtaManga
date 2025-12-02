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
            var dto = request.MangaDto;
            var manga = new Content
            {
                Title = dto.Title,
                Stock = dto.Stock,
                Synopsis = dto.Synopsis,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                AuthorId = dto.AuthorId
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
