using System.Collections.Generic;
using System;

namespace OtoMangaStore.Domain.Models
{
    public class content 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public int Stock { get; set; } 
        public string Synopsis { get; set; }
        public string ImageUrl { get; set; }
        
        // FK de Categoría
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        // FK de Autor
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        
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