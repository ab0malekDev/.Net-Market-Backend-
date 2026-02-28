using BimMarket.Application.Admin.Orders;
using BimMarket.Application.Common.Models;

namespace BimMarket.Application.Common.Abstractions;

public interface IOrderRepository
{
    Task<PagedResponse<OrderDto>> GetOrdersAsync(int page, int pageSize, string? status, CancellationToken ct = default);
    Task<OrderDetailDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<OrderDetailDto?> UpdateStatusAsync(Guid id, string status, string? notes, CancellationToken ct = default);
}
