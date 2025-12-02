using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using System.Linq;

namespace OtoMangaStore.Application.UseCases.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllCategoriesHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _uow.Categories.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
