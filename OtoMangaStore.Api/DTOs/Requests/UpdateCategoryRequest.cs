using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.DTOs.Requests
{
    public class UpdateCategoryRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
