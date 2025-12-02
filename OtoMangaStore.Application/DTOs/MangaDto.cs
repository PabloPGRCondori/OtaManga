namespace OtoMangaStore.Application.DTOs
{
    public class MangaDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Stock { get; set; } 
        public string Synopsis { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        
        public int CategoryId { get; set; } // Nuevo FK
        public string CategoryName { get; set; } = string.Empty;
        
        public int AuthorId { get; set; } // Nuevo FK
        public string AuthorName { get; set; } = string.Empty;

        public decimal CurrentPrice { get; set; }
    }
}
