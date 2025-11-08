using System.Collections.Generic;

namespace OtoMangaStore.Domain.Models
{
    // Mapea la tabla 'mangas'
    public class Manga
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } // Esencial para la lógica de negocio (OrderService)
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        
        // Propiedad de Navegación (para EF Core, no para el negocio puro)
        public ICollection<OrderItem> OrderItems { get; set; }
        
        // Regla de Negocio
        public void DecreaseStock(int quantity)
        {
            if (Stock < quantity)
            {
                throw new InvalidOperationException("Stock insuficiente.");
            }
            Stock -= quantity;
        }
    }
}