using System.Collections.Generic;

namespace OtoMangaStore.Application.DTOs
{
    public class CreateOrderDto
    {
        public List<CartItemDto> CartItems { get; set; }
    }
}
