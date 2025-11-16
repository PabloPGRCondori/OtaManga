using System.Collections.Generic;

namespace OtoMangaStore.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // [CORREGIDO] Usar la clase 'Manga'
        public ICollection<content> Mangas { get; set; } = new List<content>();
    }
}