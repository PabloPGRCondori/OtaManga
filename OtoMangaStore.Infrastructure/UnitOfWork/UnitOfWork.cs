using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Infrastructure;
using OtoMangaStore.Infrastructure.Persistence;
using OtoMangaStore.Infrastructure.Repositories;

namespace OtoMangaStore.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly OtoDbContext _context;

    // Exponer repositorios concretos vía propiedades de la interfaz
    public IMangaRepository Mangas { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(
        OtoDbContext context,
        IMangaRepository mangaRepository,
        IOrderRepository orderRepository)
    {
        _context = context;
        Mangas = mangaRepository;
        Orders = orderRepository;
    }

    // Variante con cancellation token opcional
    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();

    // Sobrecarga útil si en el futuro amplías la interfaz
    public Task<int> SaveChangesAsync(CancellationToken ct) => _context.SaveChangesAsync(ct);
}