using System;

namespace OtoMangaStore.Domain.Models
{
    public class ClickMetric
    {
        public int Id { get; set; }

        public int MangaId { get; set; } // FK

        // [CORREGIDO] Usar la clase 'Manga'
        public Content Content { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public DateTime ClickDate { get; set; }
    }
}
