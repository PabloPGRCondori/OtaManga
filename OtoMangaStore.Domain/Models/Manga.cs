using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Domain.Models
{
    public class Manga
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; }

        [Range(1, 9999, ErrorMessage = "El stock debe ser mayor a 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La sinopsis es obligatoria.")]
        [StringLength(800, ErrorMessage = "La sinopsis no puede superar los 800 caracteres.")]
        public string Synopsis { get; set; }

        [Required(ErrorMessage = "La imagen es obligatoria.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un autor.")]
        public int AuthorId { get; set; }

        public bool IsActive { get; set; }
    }
}