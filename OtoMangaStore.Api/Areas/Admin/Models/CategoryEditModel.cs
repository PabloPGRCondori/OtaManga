using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.Areas.Admin.Models
{
    public class CategoryEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Name { get; set; } = string.Empty;
    }
}
