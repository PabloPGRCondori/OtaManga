using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateCategoryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _uow.Categories.GetByIdAsync(request.Id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
            }

            category.Name = request.Name;

            await _uow.Categories.UpdateAsync(category);
            await _uow.SaveChangesAsync();
        }
    }
}
