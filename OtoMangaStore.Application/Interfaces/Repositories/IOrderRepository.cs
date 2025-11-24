using System.Collections.Generic;
using System.Threading.Tasks;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
    }
}