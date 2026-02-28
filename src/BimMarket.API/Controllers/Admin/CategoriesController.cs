using BimMarket.Application.Admin.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BimMarket.API.Controllers.Admin;

[ApiController]
[Route("api/admin/categories")]
// [Authorize]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] bool includeChildren = true, CancellationToken ct = default)
    {
        var list = await _mediator.Send(new GetCategoriesQuery(includeChildren), ct);
        return Ok(list);
    }
}
