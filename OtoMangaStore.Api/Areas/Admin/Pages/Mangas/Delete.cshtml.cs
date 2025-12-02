using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Application.UseCases.Mangas.Queries.GetMangaById;
using OtoMangaStore.Application.UseCases.Mangas.Commands.DeleteManga;
using OtoMangaStore.Application.DTOs;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public MangaDto Manga { get; set; } = new MangaDto();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _mediator.Send(new GetMangaByIdQuery(id));
            if (item == null)
                return NotFound();

            Manga = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _mediator.Send(new DeleteMangaCommand(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            TempData["Success"] = "Manga eliminado correctamente";

            return RedirectToPage("Index");
        }
    }
}
