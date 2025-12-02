using MediatR;
using OtoMangaStore.Application.DTOs;

namespace OtoMangaStore.Application.UseCases.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderDto OrderDto { get; }

        public CreateOrderCommand(CreateOrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
