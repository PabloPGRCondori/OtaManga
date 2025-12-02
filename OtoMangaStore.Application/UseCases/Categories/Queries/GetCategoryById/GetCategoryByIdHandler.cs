using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IUnitOfWork _uow;

        public GetCategoryByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _uow.Categories.GetByIdAsync(request.Id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}
