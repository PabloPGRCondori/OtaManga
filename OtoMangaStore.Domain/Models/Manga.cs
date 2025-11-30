using System;

namespace OtoMangaStore.Domain.Models
{
    public class Manga
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int Stock { get; set; }
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        // Relaciones
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}