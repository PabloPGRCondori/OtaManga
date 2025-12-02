using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.UpdateManga
{
    public class UpdateMangaHandler : IRequestHandler<UpdateMangaCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public UpdateMangaHandler(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        public async Task Handle(UpdateMangaCommand request, CancellationToken cancellationToken)
        {
            var dto = request.MangaDto;
            var manga = await _uow.Mangas.GetByIdAsync(dto.Id);
            
            if (manga == null)
            {
                throw new KeyNotFoundException($"Manga with ID {dto.Id} not found.");
            }

            manga.Title = dto.Title;
            manga.Stock = dto.Stock;
            manga.Synopsis = dto.Synopsis;
            manga.ImageUrl = dto.ImageUrl;
            manga.CategoryId = dto.CategoryId;
            manga.AuthorId = dto.AuthorId;

            await _uow.Mangas.UpdateAsync(manga);
            await _uow.SaveChangesAsync();
            
            // Invalidate cache
            _cache.Remove($"manga_{dto.Id}");
        }
    }
}
