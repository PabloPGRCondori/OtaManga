using System.Collections.Generic;

namespace OtoMangaStore.Application.DTOs
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } 
        
        public List<CartItemDto> Items { get; set; }
    }
}