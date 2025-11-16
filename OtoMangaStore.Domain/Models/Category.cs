using System.Collections.Generic;

namespace OtoMangaStore.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // [CORREGIDO] Usar la clase 'Manga'
        public ICollection<content> Mangas { get; set; } = new List<content>();
    }
}