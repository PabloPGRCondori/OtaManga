using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Domain.Models
{
    public class Manga
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no debe superar los 150 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número igual o mayor a 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La sinopsis es obligatoria.")]
        public string Synopsis { get; set; }

        [Url(ErrorMessage = "Debe ingresar una URL válida para la imagen.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un autor.")]
        public int AuthorId { get; set; }

        public bool IsActive { get; set; }

        // Relaciones
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}