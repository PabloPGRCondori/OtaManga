using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Mangas
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public List<Content> Items { get; set; } = new();
        public Dictionary<int, decimal> Prices { get; set; } = new();

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task OnGetAsync()
        {
            var items = (await _uow.Mangas.GetAllAsync()).ToList();
            Items = items;
            var pricesTasks = items.Select(m => _uow.PriceHistory.GetCurrentPriceAsync(m.Id)).ToArray();
            var prices = await Task.WhenAll(pricesTasks);
            for (int i = 0; i < items.Count; i++)
            {
                Prices[items[i].Id] = prices[i];
            }
        }
    }
}
