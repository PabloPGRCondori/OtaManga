using MediatR;

namespace OtoMangaStore.Application.UseCases.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; }

        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
