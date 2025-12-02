using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.DTOs.Mangas;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace OtoMangaStore.Application.Services
{
    public class MangaService : IMangaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public MangaService(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        public async Task<IEnumerable<MangaDto>> GetMangasByCategoryAsync(int categoryId)
        {
            string cacheKey = $"manga_cat_{categoryId}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);

                var items = await _uow.Mangas.GetMangaByCategoryAsync(categoryId);
                var list = items.ToList();

                // Fix: N+1 issue resolved using bulk retrieval
                var mangaIds = list.Select(m => m.Id).ToList();
                var prices = await _uow.PriceHistory.GetCurrentPricesForMangasAsync(mangaIds);

                return list.Select(m => new MangaDto
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
                    CurrentPrice = prices.ContainsKey(m.Id) ? prices[m.Id] : 0m
                });
            }) ?? Enumerable.Empty<MangaDto>();
        }

        public async Task<MangaDto?> GetMangaByIdAsync(int id)
        {
            string cacheKey = $"manga_{id}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);

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
            });
        }

        public async Task<MangaDto> CreateMangaAsync(CreateMangaDto dto)
        {
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

            // Invalidate cache if necessary, or let it expire.
            // For simplicity, we are not manually invalidating specific cache keys here yet, 
            // but in a real scenario we might want to remove "manga_cat_{id}" keys.

            return new MangaDto
            {
                Id = manga.Id,
                Title = manga.Title,
                Stock = manga.Stock,
                Synopsis = manga.Synopsis,
                ImageUrl = manga.ImageUrl,
                CategoryId = manga.CategoryId,
                AuthorId = manga.AuthorId,
                CurrentPrice = 0 // New manga has no price history yet
            };
        }

        public async Task UpdateMangaAsync(UpdateMangaDto dto)
        {
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
            
            // Invalidate cache for this manga
            _cache.Remove($"manga_{dto.Id}");
        }

        public async Task DeleteMangaAsync(int id)
        {
            var manga = await _uow.Mangas.GetByIdAsync(id);
            if (manga == null)
            {
                throw new KeyNotFoundException($"Manga with ID {id} not found.");
            }

            await _uow.Mangas.DeleteAsync(manga);
            await _uow.SaveChangesAsync();

            // Invalidate cache for this manga
            _cache.Remove($"manga_{id}");
        }
    }
}
