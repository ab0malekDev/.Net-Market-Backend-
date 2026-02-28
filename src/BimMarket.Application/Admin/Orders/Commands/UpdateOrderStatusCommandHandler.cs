using BimMarket.Application.Admin.Orders;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Orders.Commands;

public class UpdateOrderStatusCommandHandler(IOrderRepository repo) : IRequestHandler<UpdateOrderStatusCommand, OrderDetailDto?>
{
    public Task<OrderDetailDto?> Handle(UpdateOrderStatusCommand request, CancellationToken ct) =>
        Guid.TryParse(request.OrderId, out var guid)
            ? repo.UpdateStatusAsync(guid, request.Status, request.Notes, ct)
            : Task.FromResult<OrderDetailDto?>(null);
}
