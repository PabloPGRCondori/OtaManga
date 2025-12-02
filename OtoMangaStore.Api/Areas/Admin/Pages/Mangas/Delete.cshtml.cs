using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Services;
using OtoMangaStore.Application.DTOs;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class DeleteModel : PageModel
    {
        private readonly IMangaService _mangaService;

        public DeleteModel(IMangaService mangaService)
        {
            _mangaService = mangaService;
        }

        [BindProperty]
        public MangaDto Manga { get; set; } = new MangaDto();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var item = await _mangaService.GetMangaByIdAsync(id);
            if (item == null)
                return NotFound();

            Manga = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _mangaService.DeleteMangaAsync(id);
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
