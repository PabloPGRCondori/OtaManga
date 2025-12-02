using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IUnitOfWork _uow;

        public CreateAuthorHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = request.Name,
                Description = request.Description
            };

            await _uow.Authors.AddAsync(author);
            await _uow.SaveChangesAsync();

            return author.Id;
        }
    }
}
