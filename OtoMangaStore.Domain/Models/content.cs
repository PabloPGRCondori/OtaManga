using System.Collections.Generic;
using System;

namespace OtoMangaStore.Domain.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        
        public int Stock { get; set; } 
        public string Synopsis { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        // FK de Categoría
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        // FK de Autor
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
        
        // Propiedad de Navegación
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        // Regla de Negocio
        public void DecreaseStock(int quantity)
        {
            if (Stock < quantity)
            {
                throw new InvalidOperationException("Stock insuficiente.");
            }
            Stock -= quantity;
        }
    }
}
