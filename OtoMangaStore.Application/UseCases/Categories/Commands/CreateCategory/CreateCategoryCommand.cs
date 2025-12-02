using MediatR;
using OtoMangaStore.Application.DTOs.Categories;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public CreateCategoryDto CategoryDto { get; }

        public CreateCategoryCommand(CreateCategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }
}
