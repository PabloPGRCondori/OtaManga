using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<AuthorDto>
    {
        public int Id { get; }

        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
