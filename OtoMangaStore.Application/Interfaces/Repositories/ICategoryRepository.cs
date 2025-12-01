using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}