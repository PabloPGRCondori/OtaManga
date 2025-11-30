using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IMangaRepository Mangas { get; }
    IPriceHistoryRepository PriceHistory { get; }
    IOrderRepository Orders { get; }  // Agregar esta propiedad
    IClickMetricsRepository ClickMetrics { get; }  // Agregar esta propiedad
    Task<int> SaveChangesAsync();
}