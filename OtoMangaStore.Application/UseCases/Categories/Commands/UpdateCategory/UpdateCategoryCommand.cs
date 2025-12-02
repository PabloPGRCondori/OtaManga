using MediatR;


namespace OtoMangaStore.Application.UseCases.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
