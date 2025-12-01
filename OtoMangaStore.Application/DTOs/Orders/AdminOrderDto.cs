using System;
using System.Collections.Generic;

namespace OtoMangaStore.Application.DTOs.Orders
{
    public class AdminOrderDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<AdminOrderDetailDto> Items { get; set; } = new();
    }

    public class AdminOrderDetailDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
