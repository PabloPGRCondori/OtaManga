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
            var dto = request.AuthorDto;
            var author = await _uow.Authors.GetByIdAsync(dto.Id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {dto.Id} not found.");
            }

            author.Name = dto.Name;
            author.Description = dto.Description;

            await _uow.Authors.UpdateAsync(author);
            await _uow.SaveChangesAsync();
        }
    }
}
