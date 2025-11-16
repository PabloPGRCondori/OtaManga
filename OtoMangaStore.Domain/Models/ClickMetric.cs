using System;

namespace OtoMangaStore.Domain.Models
{
    public class ClickMetric
    {
        public int Id { get; set; }

        public int MangaId { get; set; } // FK

        // [CORREGIDO] Usar la clase 'Manga'
        public content Content { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime ClickDate { get; set; }
    }
}