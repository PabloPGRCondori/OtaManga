using MediatR;


namespace OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string ExternalUserId { get; set; } = string.Empty;
        public List<OrderItemCommand> Items { get; set; } = new();
    }

    public class OrderItemCommand
    {
        public int MangaId { get; set; }
        public int Quantity { get; set; }
    }
}
