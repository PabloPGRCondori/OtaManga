using MediatR;


namespace OtoMangaStore.Application.UseCases.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }
}
