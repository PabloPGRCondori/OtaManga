using MediatR;

namespace OtoMangaStore.Application.UseCases.Mangas.Commands.DeleteManga
{
    public class DeleteMangaCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteMangaCommand(int id)
        {
            Id = id;
        }
    }
}
