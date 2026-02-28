using BimMarket.Application.Admin.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/products")]
// [Authorize] // re-enable when auth is fixed
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("")]
    public async Task<IActionResult> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? categoryId = null, [FromQuery] string? search = null, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetProductsQuery(page, pageSize, categoryId, search), ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out _))
            return BadRequest(new { error = "Invalid product id format." });
        var product = await _mediator.Send(new GetProductByIdQuery(id), ct);
        if (product == null) return NotFound();
        return Ok(product);
    }
}
