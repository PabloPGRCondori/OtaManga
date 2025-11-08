using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IMangaRepository Mangas { get; }

    Task<int> SaveChangesAsync();
}