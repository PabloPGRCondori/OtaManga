using MediatR;
using OtoMangaStore.Application.DTOs.Categories;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public UpdateCategoryDto CategoryDto { get; }

        public UpdateCategoryCommand(UpdateCategoryDto categoryDto)
        {
            CategoryDto = categoryDto;
        }
    }
}
