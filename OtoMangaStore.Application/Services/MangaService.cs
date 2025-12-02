using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;

namespace OtoMangaStore.Application.Services
{
    public class MangaService : IMangaService
    {
        private readonly IUnitOfWork _uow;

        public MangaService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<MangaDto>> GetMangasByCategoryAsync(int categoryId)
        {
            var items = await _uow.Mangas.GetMangaByCategoryAsync(categoryId);
            var list = items.ToList();

            // Note: N+1 issue still exists here, will be fixed in Phase 2
            var prices = await Task.WhenAll(list.Select(m => _uow.PriceHistory.GetCurrentPriceAsync(m.Id)));

            return list.Select((m, idx) => new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name ?? string.Empty,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name ?? string.Empty,
                CurrentPrice = prices[idx]
            });
        }

        public async Task<MangaDto?> GetMangaByIdAsync(int id)
        {
            var m = await _uow.Mangas.GetMangaDetailsAsync(id);
            if (m == null) return null;

            var price = await _uow.PriceHistory.GetCurrentPriceAsync(m.Id);
            
            return new MangaDto
            {
                Id = m.Id,
                Title = m.Title,
                Stock = m.Stock,
                Synopsis = m.Synopsis,
                ImageUrl = m.ImageUrl,
                CategoryId = m.CategoryId,
                CategoryName = m.Category?.Name ?? string.Empty,
                AuthorId = m.AuthorId,
                AuthorName = m.Author?.Name ?? string.Empty,
                CurrentPrice = price
            };
        }
    }
}
