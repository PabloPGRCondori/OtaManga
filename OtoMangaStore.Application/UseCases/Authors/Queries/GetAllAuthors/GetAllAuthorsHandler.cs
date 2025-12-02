using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.Interfaces.Repositories;
using System.Linq;

namespace OtoMangaStore.Application.UseCases.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllAuthorsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _uow.Authors.GetAllAsync();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });
        }
    }
}
