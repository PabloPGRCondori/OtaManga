using MediatR;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand : IRequest
    {
        public int Id { get; }

        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }
    }
}
