using MediatR;

namespace BimMarket.Application.Admin.Orders.Commands;

public record UpdateOrderStatusCommand(string OrderId, string Status, string? Notes) : IRequest<OrderDetailDto?>;
