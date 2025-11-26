using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IMangaRepository Mangas { get; }
        IPriceHistoryRepository PriceHistory { get; }
        IOrderRepository Orders { get; }
        IClickMetricsRepository ClickMetrics { get; }

        IAuthorRepository Authors { get; }
        ICategoryRepository Categories { get; }

        Task<int> SaveChangesAsync();
    }
}