using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.Areas.Admin.Models
{
    public class AuthorEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
