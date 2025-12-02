using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Authors.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>>
    {
    }
}
