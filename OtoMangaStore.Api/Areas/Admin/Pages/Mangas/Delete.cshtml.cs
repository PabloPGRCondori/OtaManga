using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // Usamos "content" porque ese es TU modelo real
        [BindProperty]
        public Content Manga { get; set; } = new Content();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _uow.Mangas.GetMangaDetailsAsync(id);
            if (item == null)
                return NotFound();

            Manga = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var item = await _uow.Mangas.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await _uow.Mangas.DeleteAsync(item);
            await _uow.SaveChangesAsync();

            TempData["Success"] = "Manga eliminado correctamente";

            return RedirectToPage("Index");
        }
    }
}
