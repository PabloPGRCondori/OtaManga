using MediatR;


namespace OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
