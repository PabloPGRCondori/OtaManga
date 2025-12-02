using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;

using OtoMangaStore.Application.UseCases.Authors.Commands.CreateAuthor;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public AuthorEditModel Input { get; set; } = new AuthorEditModel();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var command = new CreateAuthorCommand
            {
                Name = Input.Name,
                Description = Input.Description
            };

            await _mediator.Send(command);

            return RedirectToPage("Index");
        }
    }
}
