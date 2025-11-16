using System.Threading.Tasks;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    // =======================================================================================
    // TAREA - INTERFAZ DE PRECIOS
    // 
    // La implementación de esta interfaz (en la capa Infrastructure) debe consultar la
    // tabla 'price_history' para encontrar el precio más reciente para el Manga/Content Item.
    // =======================================================================================
    public interface IPriceHistoryRepository
    {
        Task<decimal> GetCurrentPriceAsync(int mangaId);
        // Se puede añadir un método para registrar un nuevo precio si es necesario.
    }
}