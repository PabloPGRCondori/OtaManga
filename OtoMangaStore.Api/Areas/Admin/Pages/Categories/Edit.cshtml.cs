using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public EditModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public async Task<IActionResult> OnGet(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);

            if (c == null)
                return RedirectToPage("Index");

            Category = c;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _uow.Categories.UpdateAsync(Category);
            await _uow.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}
