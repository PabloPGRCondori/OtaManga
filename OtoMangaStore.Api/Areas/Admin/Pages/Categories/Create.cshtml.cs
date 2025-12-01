using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Domain.Models;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public CreateModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _uow.Categories.AddAsync(Category);
            await _uow.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}