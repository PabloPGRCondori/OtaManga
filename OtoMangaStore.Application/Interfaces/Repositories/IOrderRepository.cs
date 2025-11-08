using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
