using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Application.UseCases.Authors.Commands.DeleteAuthor;
using OtoMangaStore.Application.UseCases.Authors.Queries.GetAuthorById;
using OtoMangaStore.Domain.Models;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Author Author { get; set; } = new Author();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var authorDto = await _mediator.Send(new GetAuthorByIdQuery(id));
            if (authorDto == null) return NotFound();

            Author = new Author { Id = authorDto.Id, Name = authorDto.Name, Description = authorDto.Description };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _mediator.Send(new DeleteAuthorCommand(id));
            return RedirectToPage("Index");
        }
    }
}
