// OtoMangaStore.Domain/Entities/OrderItem.cs

namespace OtoMangaStore.Domain.Models
{
    // Mapea la tabla 'OrderItems'
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MangaId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navegación
        public Order Order { get; set; }
        public Manga Manga { get; set; }
    }
}