namespace OtoMangaStore.Application.DTOs
{
    public class MangaDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Stock { get; set; } 
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        
        public int CategoryId { get; set; } // Nuevo FK
        public string CategoryName { get; set; }
        
        public int AuthorId { get; set; } // Nuevo FK
        public string AuthorName { get; set; } 

        public decimal CurrentPrice { get; set; }
    }
}