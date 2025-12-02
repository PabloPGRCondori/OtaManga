using System.ComponentModel.DataAnnotations;

namespace OtoMangaStore.Api.DTOs.Requests
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; } = string.Empty;
        [Required]
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderItemRequest
    {
        [Required]
        public int MangaId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
