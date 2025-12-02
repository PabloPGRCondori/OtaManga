using MediatR;
using OtoMangaStore.Domain.Models;
using System.Collections.Generic;

namespace OtoMangaStore.Application.UseCases.Orders.Queries.GetOrdersByUser
{
    public class GetOrdersByUserQuery : IRequest<IEnumerable<Order>>
    {
        public string UserId { get; }

        public GetOrdersByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
}
