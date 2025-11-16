using Microsoft.AspNetCore.Identity;

namespace OtoMangaStore.Domain.Models
{
    // Esta clase hereda toda la funcionalidad de usuario (login, password hash, tokens) 
    // necesaria para Identity.
    public class ApplicationUser : IdentityUser
    {
        // NOTA: No es estrictamente necesario, pero es buena práctica añadir 
        // una propiedad de navegación a la métrica, ya que el usuario es parte de la métrica.

        // Propiedad de navegación (Opcional, pero completa la relación de ClickMetric)
        // public ICollection<ClickMetric> ClickMetrics { get; set; } = new List<ClickMetric>();
    }
}