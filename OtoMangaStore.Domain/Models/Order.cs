// OtoMangaStore.Domain/Entities/Order.cs
using System;
using System.Collections.Generic;

namespace OtoMangaStore.Domain.Models
{
    // Mapea la tabla 'Orders'
    public class Order
    {
        public int Id { get; set; }
        public string ExternalUserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}