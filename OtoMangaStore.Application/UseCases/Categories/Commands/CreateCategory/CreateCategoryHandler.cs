using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CreateCategoryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            await _uow.Categories.AddAsync(category);
            await _uow.SaveChangesAsync();

            return category.Id;
        }
    }
}
