using System.Threading.Tasks;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure.Persistence;
using OtoMangaStore.Infrastructure.Repositories;

namespace OtoMangaStore.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OtoDbContext _context;

        public UnitOfWork(OtoDbContext context)
        {
            _context = context;
            Mangas = new MangaRepository(_context);
            PriceHistory = new PriceHistoryRepository(_context);
            Orders = new OrderRepository(_context);
            ClickMetrics = new ClickMetricsRepository(_context);
        }

        public IMangaRepository Mangas { get; }
        public IPriceHistoryRepository PriceHistory { get; }
        public IOrderRepository Orders { get; private set; }
        public IClickMetricsRepository ClickMetrics { get; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
