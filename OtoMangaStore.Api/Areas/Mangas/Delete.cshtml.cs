using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public content Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _uow.Mangas.GetMangaDetailsAsync(id);
            if (Item == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var item = await _uow.Mangas.GetByIdAsync(id);
            if (item == null) return NotFound();

            // Antes de eliminar, verificar relaciones (orderItems, pricehistory, etc.) si aplica
            await _uow.Mangas.DeleteAsync(item);
            await _uow.SaveChangesAsync();

            TempData["Success"] = "Contenido eliminado";
            return RedirectToPage("Index");
        }
    }
}