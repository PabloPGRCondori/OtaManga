using System;

namespace OtoMangaStore.Domain.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }
        
        public int MangaId { get; set; } // Clave Foránea
        public content Content { get; set; } // Navegación

        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}