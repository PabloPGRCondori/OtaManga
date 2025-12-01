using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Author Author { get; set; } = new Author();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var author = await _uow.Authors.GetByIdAsync(id);
            if (author == null) return NotFound();

            Author = author;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var author = await _uow.Authors.GetByIdAsync(id);

            if (author != null)
            {
                await _uow.Authors.DeleteAsync(author);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
