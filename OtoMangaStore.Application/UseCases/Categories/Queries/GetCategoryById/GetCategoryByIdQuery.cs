using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
