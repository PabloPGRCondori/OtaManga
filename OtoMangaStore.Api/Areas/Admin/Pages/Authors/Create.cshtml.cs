using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;
using OtoMangaStore.Application.DTOs.Authors;
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

            var dto = new CreateAuthorDto
            {
                Name = Input.Name,
                Description = Input.Description
            };

            await _mediator.Send(new CreateAuthorCommand(dto));

            return RedirectToPage("Index");
        }
    }
}
