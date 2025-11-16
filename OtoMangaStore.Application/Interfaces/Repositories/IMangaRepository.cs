using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OtoMangaStore.Application.Interfaces.Repositories
{
    // NOTA: Esta interfaz maneja la entidad 'Manga', que está mapeada a la tabla 'content' (Ítems de Catálogo).
    // Se recomienda renombrar la clase/interfaz a IContentRepository en una refactorización futura.
    public interface IMangaRepository 
    {
        // 1. Funcionalidad de Lectura: Obtener el Manga (Content Item) con sus datos normalizados
        // [TAREA PARA OTRO MIEMBRO DEL EQUIPO]: La implementación de este método (en la capa Infrastructure)
        // debe usar EF Core .Include() para cargar las entidades Category y Author.
        Task<content> GetMangaDetailsAsync(int mangaId);
        
        // 2. Funcionalidad de Lectura: Obtener todos los Mangas (Content Items) filtrados por Categoría
        // [TAREA PARA OTRO MIEMBRO DEL EQUIPO]: La implementación de este método debe usar EF Core
        // .Where(m => m.CategoryId == categoryId) para filtrar.
        Task<IEnumerable<content>> GetMangaByCategoryAsync(int categoryId);
        
        // 3. Funcionalidad de Escritura: Método de actualización para la entidad Manga
        // Esto es necesario para que el OrderService pueda guardar los cambios de Stock.
        Task UpdateAsync(content manga); 

        // 4. Funcionalidad base: Obtener por ID (necesario para el OrderService)
        Task<content> GetByIdAsync(int mangaId); 
    }
}