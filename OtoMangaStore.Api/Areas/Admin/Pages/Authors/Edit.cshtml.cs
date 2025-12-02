using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Api.Areas.Admin.Models;

using OtoMangaStore.Application.UseCases.Authors.Commands.UpdateAuthor;
using OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public AuthorEditModel Input { get; set; } = new AuthorEditModel();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery(id));

            if (author == null)
            {
                return NotFound();
            }

            Input = new AuthorEditModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var command = new UpdateAuthorCommand
            {
                Id = Input.Id,
                Name = Input.Name,
                Description = Input.Description
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
