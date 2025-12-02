using MediatR;
using OtoMangaStore.Application.DTOs.Authors;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public CreateAuthorDto AuthorDto { get; }

        public CreateAuthorCommand(CreateAuthorDto authorDto)
        {
            AuthorDto = authorDto;
        }
    }
}
