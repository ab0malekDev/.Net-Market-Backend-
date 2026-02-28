using BimMarket.Application.Admin.Inventory;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Inventory.Queries;

public class GetInventoryQueryHandler(IInventoryRepository repo) : IRequestHandler<GetInventoryQuery, List<InventoryDto>>
{
    public Task<List<InventoryDto>> Handle(GetInventoryQuery request, CancellationToken ct) =>
        repo.GetAsync(request.BranchId, request.ProductId, ct);
}
