using BimMarket.Application.Common.Models;
using MediatR;

namespace BimMarket.Application.Admin.Orders.Queries;

public record GetOrdersQuery(int Page = 1, int PageSize = 20, string? Status = null)
    : IRequest<PagedResponse<OrderDto>>;

public record GetOrderByIdQuery(string Id) : IRequest<OrderDetailDto?>;
