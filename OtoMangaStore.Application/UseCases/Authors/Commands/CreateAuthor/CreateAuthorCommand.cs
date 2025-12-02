using MediatR;


namespace OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
