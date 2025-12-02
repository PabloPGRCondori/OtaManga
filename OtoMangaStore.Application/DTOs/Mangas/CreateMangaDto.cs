using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Application.DTOs.Mangas
{
    public class CreateMangaDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(200, ErrorMessage = "El título no debe superar los 200 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La sinopsis es obligatoria.")]
        public string Synopsis { get; set; } = string.Empty;

        [Url(ErrorMessage = "La URL de la imagen no es válida.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio.")]
        public int AuthorId { get; set; }
    }
}
