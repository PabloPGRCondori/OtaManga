using MediatR;
using OtoMangaStore.Application.Interfaces.Repositories;
using OtoMangaStore.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OtoMangaStore.Application.UseCases.Orders.Queries.GetOrdersByUser
{
    public class GetOrdersByUserHandler : IRequestHandler<GetOrdersByUserQuery, IEnumerable<Order>>
    {
        private readonly IUnitOfWork _uow;

        public GetOrdersByUserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Orders.GetByUserIdAsync(request.UserId);
        }
    }
}
