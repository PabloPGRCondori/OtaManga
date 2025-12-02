using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;
using OtoMangaStore.Application.UseCases.Categories.Commands.CreateCategory;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CategoryEditModel Input { get; set; } = new CategoryEditModel();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var command = new CreateCategoryCommand
            {
                Name = Input.Name
            };

            await _mediator.Send(command);

            return RedirectToPage("Index");
        }
    }
}