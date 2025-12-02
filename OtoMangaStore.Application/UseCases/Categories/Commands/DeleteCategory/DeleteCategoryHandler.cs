using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _uow;

        public DeleteCategoryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _uow.Categories.GetByIdAsync(request.Id);
            if (category != null)
            {
                await _uow.Categories.DeleteAsync(category);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
