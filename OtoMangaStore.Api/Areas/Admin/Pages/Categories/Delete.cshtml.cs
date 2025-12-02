using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public DeleteModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Category Category { get; set; } = new Category();

        public async Task<IActionResult> OnGet(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);

            if (c == null)
                return RedirectToPage("Index");

            Category = c;
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);

            if (category != null)
            {
                await _uow.Categories.DeleteAsync(category);
                await _uow.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
