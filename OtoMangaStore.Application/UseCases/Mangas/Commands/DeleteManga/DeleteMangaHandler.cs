using MediatR;
using Microsoft.Extensions.Caching.Memory;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.DeleteManga
{
    public class DeleteMangaHandler : IRequestHandler<DeleteMangaCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public DeleteMangaHandler(IUnitOfWork uow, IMemoryCache cache)
        {
            _uow = uow;
            _cache = cache;
        }

        public async Task Handle(DeleteMangaCommand request, CancellationToken cancellationToken)
        {
            var manga = await _uow.Mangas.GetByIdAsync(request.Id);
            if (manga == null)
            {
                throw new KeyNotFoundException($"Manga with ID {request.Id} not found.");
            }

            await _uow.Mangas.DeleteAsync(manga);
            await _uow.SaveChangesAsync();

            // Invalidate cache
            _cache.Remove($"manga_{request.Id}");
        }
    }
}
