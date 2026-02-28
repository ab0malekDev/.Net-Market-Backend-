using BimMarket.Application.Admin.Orders.Commands;
using BimMarket.Application.Admin.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/orders")]
// [Authorize]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("")]
    public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? status = null, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetOrdersQuery(page, pageSize, status), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest(new { error = "Invalid order id format." });
        var order = await _mediator.Send(new GetOrderByIdQuery(id), ct);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] BimMarket.Application.Admin.Orders.UpdateOrderStatusRequest request, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest(new { error = "Invalid order id format." });
        var result = await _mediator.Send(new UpdateOrderStatusCommand(id, request.Status, request.Notes), ct);
        if (result == null) return NotFound();
        return Ok(result);
    }
}
