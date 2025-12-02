using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtoMangaStore.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no debe superar los 500 caracteres.")]
        public string Description { get; set; } = string.Empty;

        public ICollection<Content> Mangas { get; set; } = new List<Content>();
    }
}
