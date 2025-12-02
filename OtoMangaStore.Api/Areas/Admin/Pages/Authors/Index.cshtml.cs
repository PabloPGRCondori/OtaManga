using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.UseCases.Authors.Queries.GetAllAuthors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<AuthorDto> Authors { get; set; } = new List<AuthorDto>();

        public async Task OnGetAsync()
        {
            Authors = await _mediator.Send(new GetAllAuthorsQuery());
        }
    }
}
