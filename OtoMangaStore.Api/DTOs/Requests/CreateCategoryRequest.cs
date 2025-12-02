using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.DTOs.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
