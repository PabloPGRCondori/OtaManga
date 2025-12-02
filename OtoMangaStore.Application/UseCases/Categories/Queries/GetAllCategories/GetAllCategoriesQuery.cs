using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
