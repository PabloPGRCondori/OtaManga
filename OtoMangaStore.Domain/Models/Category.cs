using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(80, ErrorMessage = "El nombre no debe superar los 80 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no debe superar los 200 caracteres.")]
        public string Description { get; set; }
    }
}