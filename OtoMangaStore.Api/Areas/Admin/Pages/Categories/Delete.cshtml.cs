using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Application.UseCases.Categories.Commands.DeleteCategory;
using OtoMangaStore.Application.UseCases.Categories.Queries.GetCategoryById;
using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Category Category { get; set; } = new Category();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);

            if (c == null)
                return RedirectToPage("Index");

            Category = c;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return RedirectToPage("Index");
        }
    }
}
