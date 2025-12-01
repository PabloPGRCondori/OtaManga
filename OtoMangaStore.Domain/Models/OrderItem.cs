namespace OtoMangaStore.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MangaId { get; set; } // FK
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navegación
        public Order Order { get; set; } = null!;
        // [CORREGIDO] Usar la clase 'Manga'
        public Content Content { get; set; } = null!; 
    }
}
