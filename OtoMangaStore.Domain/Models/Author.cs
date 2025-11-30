using System.Collections.Generic;

namespace OtoMangaStore.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relación correcta
        public ICollection<Manga> Mangas { get; set; } = new List<Manga>();
    }
}