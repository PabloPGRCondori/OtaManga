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
        public Category Category { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            Category = await _uow.Categories.GetByIdAsync(id);

            if (Category == null)
                return RedirectToPage("Index");

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