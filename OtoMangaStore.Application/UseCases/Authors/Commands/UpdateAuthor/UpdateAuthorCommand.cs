using MediatR;
using OtoMangaStore.Application.DTOs.Authors;

namespace OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        public UpdateAuthorDto AuthorDto { get; }

        public UpdateAuthorCommand(UpdateAuthorDto authorDto)
        {
            AuthorDto = authorDto;
        }
    }
}
