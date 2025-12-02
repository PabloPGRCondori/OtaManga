using System.Collections.Generic;

namespace OtoMangaStore.Application.DTOs
{
    public class CreateOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
