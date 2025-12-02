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
            var categoryDto = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (categoryDto == null) return RedirectToPage("Index");

            Category = new Category { Id = categoryDto.Id, Name = categoryDto.Name };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return RedirectToPage("Index");
        }
    }
}
