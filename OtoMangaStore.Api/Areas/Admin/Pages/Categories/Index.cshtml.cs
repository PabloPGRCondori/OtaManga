using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGet()
        {
            Categories = await _uow.Categories.GetAllAsync();
        }
    }
}