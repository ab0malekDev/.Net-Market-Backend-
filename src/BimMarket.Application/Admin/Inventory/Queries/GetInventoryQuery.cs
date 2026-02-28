using MediatR;

namespace BimMarket.Application.Admin.Inventory.Queries;

public record GetInventoryQuery(string? BranchId = null, string? ProductId = null) : IRequest<List<InventoryDto>>;
