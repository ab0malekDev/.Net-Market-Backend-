using BimMarket.Application.Admin.Inventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/inventory")]
// [Authorize]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetInventory([FromQuery] string? branchId = null, [FromQuery] string? productId = null, CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetInventoryQuery(branchId, productId), ct);
        return Ok(list);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInventory(string id, [FromBody] UpdateInventoryBody body, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var guid))
            return BadRequest(new { error = "Invalid inventory id format." });
        var repo = HttpContext.RequestServices.GetRequiredService<BimMarket.Application.Common.Abstractions.IInventoryRepository>();
        var result = await repo.UpdateAsync(guid, body.Quantity, body.MinimumThreshold, ct);
        if (result == null) return NotFound();
        return Ok(result);
    }
}

public record UpdateInventoryBody(int Quantity, int MinimumThreshold);
