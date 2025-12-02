using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateAuthorHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _uow.Authors.GetByIdAsync(request.Id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} not found.");
            }

            author.Name = request.Name;
            author.Description = request.Description;

            await _uow.Authors.UpdateAsync(author);
            await _uow.SaveChangesAsync();
        }
    }
}
