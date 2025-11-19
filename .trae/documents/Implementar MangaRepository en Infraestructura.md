## Resumen

Implementar `MangaRepository` en `OtoMangaStore.Infrastructure/Repositories/MangaRepository.cs`, inyectando `OtoDbContext` y cumpliendo `IMangaRepository` con consultas EF Core asíncronas y eager loading de `Category` y `Author` cuando aplique.

## Referencias del código

* Interfaz `IMangaRepository`: `OtoMangaStore.Application/Interfaces/Repositories/IMangaRepository.cs:14-27`

* DbContext y DbSets: `OtoMangaStore.Infrastructure/Persistence/OtoDbContext.cs:13-19`

* Mapeo de entidad `content`: `OtoMangaStore.Infrastructure/Persistence/OtoDbContext.cs:21-25`

* Patrón de repositorio existente: `OtoMangaStore.Infrastructure/Repositories/PriceHistoryRepository.cs:7-18`

* Entidad `content`: `OtoMangaStore.Domain/Models/content.cs:6-22`

## Decisiones

* Usar `AsNoTracking()` en lecturas para rendimiento, consistente con `PriceHistoryRepository`.

* Eager loading con `.Include(m => m.Category).Include(m => m.Author)` en `GetMangaDetailsAsync` y `GetMangaByCategoryAsync`.

* `UpdateAsync` usa `_db.Mangas.Update(manga)` y `await _db.SaveChangesAsync()`.

* Firmas exactamente como la interfaz (retornos `Task<content>` y `Task<IEnumerable<content>>`). Si no existe, el valor puede ser `null` según EF Core.

## Código propuesto (MangaRepository.cs)

```csharp
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Infrastructure.Persistence;

namespace OtoMangaStore.Infrastructure.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly OtoDbContext _db;

        public MangaRepository(OtoDbContext db)
        {
            _db = db;
        }

        public async Task<content> GetByIdAsync(int mangaId)
        {
            return await _db.Mangas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == mangaId);
        }

        public async Task<content> GetMangaDetailsAsync(int id)
        {
            return await _db.Mangas
                .AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<content>> GetMangaByCategoryAsync(int categoryId)
        {
            return await _db.Mangas
                .AsNoTracking()
                .Include(m => m.Category)
                .Include(m => m.Author)
                .Where(m => m.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task UpdateAsync(content manga)
        {
            _db.Mangas.Update(manga);
            await _db.SaveChangesAsync();
        }
    }
}
```

## Validación

* Compilar el proyecto para comprobar namespaces y dependencias.

* Ejecutar pruebas manuales: consultar un ID existente y no existente; filtrar por categoría; actualizar stock y verificar persistencia.

¿Confirmas que proceda
