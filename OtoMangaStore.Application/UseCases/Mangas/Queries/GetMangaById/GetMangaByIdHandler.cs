using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById
{
    public class GetMangaByIdHandler : IRequestHandler<GetMangaByIdQuery, MangaDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public GetMangaByIdHandler(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        public async Task<MangaDto?> Handle(GetMangaByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"manga_{request.Id}";
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);

                var manga = await _uow.Mangas.GetMangaDetailsAsync(request.Id);
                if (manga == null) return null;

                var currentPrice = await _uow.PriceHistory.GetCurrentPriceAsync(manga.Id);

                return new MangaDto
                {
                    Id = manga.Id,
                    Title = manga.Title,
                    Stock = manga.Stock,
                    Synopsis = manga.Synopsis,
                    ImageUrl = manga.ImageUrl,
                    CategoryId = manga.CategoryId,
                    CategoryName = manga.Category?.Name ?? "Sin Categoría",
                    AuthorId = manga.AuthorId,
                    AuthorName = manga.Author?.Name ?? "Desconocido",
                    CurrentPrice = currentPrice
                };
            });
        }
    }
}
