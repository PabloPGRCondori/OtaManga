using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;
using OtoMangaStore.Application.DTOs.Categories;
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
            {
                return NotFound();
            }

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

            var dto = new UpdateCategoryDto
            {
                Id = Input.Id,
                Name = Input.Name
            };

            try
            {
                await _mediator.Send(new UpdateCategoryCommand(dto));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
