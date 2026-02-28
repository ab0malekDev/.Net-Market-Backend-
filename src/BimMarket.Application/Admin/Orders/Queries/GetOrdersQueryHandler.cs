using BimMarket.Application.Admin.Orders;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Application.Common.Models;
using MediatR;

namespace BimMarket.Application.Admin.Orders.Queries;

public class GetOrdersQueryHandler(IOrderRepository repo) : IRequestHandler<GetOrdersQuery, PagedResponse<OrderDto>>
{
    public Task<PagedResponse<OrderDto>> Handle(GetOrdersQuery request, CancellationToken ct) =>
        repo.GetOrdersAsync(request.Page, request.PageSize, request.Status, ct);
}

public class GetOrderByIdQueryHandler(IOrderRepository repo) : IRequestHandler<GetOrderByIdQuery, OrderDetailDto?>
{
    public Task<OrderDetailDto?> Handle(GetOrderByIdQuery request, CancellationToken ct) =>
        Guid.TryParse(request.Id, out var guid)
            ? repo.GetByIdAsync(guid, ct)
            : Task.FromResult<OrderDetailDto?>(null);
}
