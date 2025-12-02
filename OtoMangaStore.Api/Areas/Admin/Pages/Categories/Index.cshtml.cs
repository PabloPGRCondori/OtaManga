using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using OtoMangaStore.Application.DTOs;
using OtoMangaStore.Application.UseCases.Categories.Queries.GetAllCategories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public async Task OnGetAsync()
        {
            Categories = await _mediator.Send(new GetAllCategoriesQuery());
        }
    }
}