using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.Areas.Admin.Models
{
    public class ContentEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [Url]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public bool IsActive { get; set; }
    }
}
