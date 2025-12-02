using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.DTOs.Requests
{
    public class CreateMangaRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
