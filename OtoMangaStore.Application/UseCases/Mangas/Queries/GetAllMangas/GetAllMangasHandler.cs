using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Mangas.Queries.GetAllMangas
{
    public class GetAllMangasHandler : IRequestHandler<GetAllMangasQuery, IEnumerable<MangaDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public GetAllMangasHandler(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        public async Task<IEnumerable<MangaDto>> Handle(GetAllMangasQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "all_mangas";
            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);

                var mangas = await _uow.Mangas.GetAllAsync();
                
                // Bulk fetch prices
                var mangaIds = mangas.Select(m => m.Id).ToList();
                var prices = await _uow.PriceHistory.GetCurrentPricesForMangasAsync(mangaIds);

                return mangas.Select(m => new MangaDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Stock = m.Stock,
                    Synopsis = m.Synopsis,
                    ImageUrl = m.ImageUrl,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category?.Name ?? "Sin Categoría",
                    AuthorId = m.AuthorId,
                    AuthorName = m.Author?.Name ?? "Desconocido",
                    CurrentPrice = prices.ContainsKey(m.Id) ? prices[m.Id] : 0
                });
            }) ?? Enumerable.Empty<MangaDto>();
        }
    }
}
