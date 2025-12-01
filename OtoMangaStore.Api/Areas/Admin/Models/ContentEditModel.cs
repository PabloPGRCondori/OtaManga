using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.Areas.Admin.Models
{
    public class ContentEditModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public string Synopsis { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public bool IsActive { get; set; }
    }
}