using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IUnitOfWork _uow;

        public DeleteAuthorHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _uow.Authors.GetByIdAsync(request.Id);
            if (author != null)
            {
                await _uow.Authors.DeleteAsync(author);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
