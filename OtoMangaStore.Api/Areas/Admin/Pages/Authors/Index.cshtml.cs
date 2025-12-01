using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Author> Authors { get; set; } = Enumerable.Empty<Author>();

        public async Task OnGetAsync()
        {
            Authors = await _uow.Authors.GetAllAsync();
        }
    }
}
