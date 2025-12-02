using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDto>
    {
        private readonly IUnitOfWork _uow;

        public GetAuthorByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _uow.Authors.GetByIdAsync(request.Id);
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description
            };
        }
    }
}
