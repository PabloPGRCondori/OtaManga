using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OtoMangaStore.Application.Interfaces.Repositories;

namespace OtoMangaStore.Api.Areas.Admin.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public int TotalMangas { get; set; }
        public int TotalOrders { get; set; }
        public int TotalClicks { get; set; }

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task OnGetAsync()
        {
            var mangas = (await _uow.Mangas.GetAllAsync()).ToList();
            TotalMangas = mangas.Count;

            var orders = (await _uow.Orders.GetByUserIdAsync(string.Empty)).ToList();
            // Nota: Orders.GetByUserIdAsync("") se usa aquí solo como aproximación. 
            // Mejor agregar un método GetAllAsync() en IOrderRepository para contar.
            TotalOrders = orders.Count;

            var clicksTop = await _uow.ClickMetrics.GetTopClickedAsync(1000);
            TotalClicks = clicksTop.Sum(x => x.Clicks);
        }
    }
}