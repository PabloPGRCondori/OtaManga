using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;

using OtoMangaStore.Application.UseCases.Categories.Commands.UpdateCategory;
using OtoMangaStore.Application.UseCases.Categories.Queries.GetCategoryById;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CategoryEditModel Input { get; set; } = new CategoryEditModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));

            if (category == null)
                return RedirectToPage("Index");

            Input = new CategoryEditModel
            {
                Id = category.Id,
                Name = category.Name
            };
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var command = new UpdateCategoryCommand
            {
                Id = Input.Id,
                Name = Input.Name
            };

            try
            {
                await _mediator.Send(command);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
