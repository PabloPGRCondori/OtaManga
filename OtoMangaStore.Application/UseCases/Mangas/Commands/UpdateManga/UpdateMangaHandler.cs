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
            var manga = await _uow.Mangas.GetByIdAsync(request.Id);
            
            if (manga == null)
            {
                throw new KeyNotFoundException($"Manga with ID {request.Id} not found.");
            }

            manga.Title = request.Title;
            manga.Stock = request.Stock;
            manga.Synopsis = request.Description;
            manga.ImageUrl = request.CoverImageUrl;
            manga.CategoryId = request.CategoryId;
            manga.AuthorId = request.AuthorId;
            manga.Price = request.Price;

            await _uow.Mangas.UpdateAsync(manga);
            await _uow.SaveChangesAsync();
            
            // Invalidate cache
            _cache.Remove($"manga_{request.Id}");
        }
    }
}
